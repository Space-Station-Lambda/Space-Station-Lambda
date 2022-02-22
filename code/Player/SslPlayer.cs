// ReSharper disable UnassignedGetOnlyAutoProperty

using Sandbox;
using ssl.Modules.Clothes;
using ssl.Modules.Inputs;
using ssl.Modules.Roles;
using ssl.Modules.Selection;
using ssl.Modules.Skills;
using ssl.Modules.Statuses;
using ssl.Player.Animators;
using ssl.Player.Cameras;
using ssl.Player.Controllers;
using SpawnPoint = ssl.Modules.Rounds.SpawnPoint;

namespace ssl.Player;

public partial class SslPlayer : Sandbox.Player, ISelectable
{
    private const string MODEL = "models/citizen/citizen.vmdl";
    private const float MAX_HEALTH = 100f;

    [BindComponent] public new PlayerInventory Inventory { get; }
    [BindComponent] public RoleHandler RoleHandler { get; }
    [BindComponent] public ClothesHandler ClothesHandler { get; }
    [BindComponent] public StatusHandler StatusHandler { get; }
    [BindComponent] public StainHandler StainHandler { get; }
    [BindComponent] public InputHandler InputHandler { get; }
    [BindComponent] public RagdollHandler RagdollHandler { get; }
    [BindComponent] public SkillHandler SkillHandler { get; }
    [BindComponent] public Dragger Dragger { get; }

    public new HumanController Controller
    {
        get => (HumanController) base.Controller;
        private set => base.Controller = value;
    }

    public void OnSelectStart(SslPlayer sslPlayer) { }

    public void OnSelectStop(SslPlayer sslPlayer) { }

    public void OnSelect(SslPlayer sslPlayer) { }

    public void OnInteract(SslPlayer sslPlayer, int strength, TraceResult hit) { }

    public override void ClientSpawn()
    {
        base.ClientSpawn();

        if (Inventory.ViewModel != null || !IsLocalPawn) return;

        Inventory.ViewModel = new HandViewModel
        {
            EnableAllCollisions = false, EnableViewmodelRendering = true, Owner = Owner
        };
        Inventory.ViewModel.SetHoldType(HoldType.None);
    }

    public override void Spawn()
    {
        base.Spawn();
        
        Health = MAX_HEALTH;
        
        Components.Create<InputHandler>();
        Components.Create<Dragger>();
        Components.Create<RagdollHandler>();
        Components.Create<PlayerInventory>();
        Components.Create<RoleHandler>();
        Components.Create<ClothesHandler>();
        Components.Create<StatusHandler>();
        Components.Create<StainHandler>();
        Components.Create<SkillHandler>();
    }

    public override void FrameSimulate(Client cl)
    {
        base.FrameSimulate(cl);
        if (Inventory.HoldingItem.IsValid()) Inventory.HoldingItem.FrameSimulate(cl);
    }

    /// <summary>
    ///     Called each tick, clientside and serverside
    /// </summary>
    /// <param name="client"></param>
    public override void Simulate(Client client)
    {
        PawnController controller = GetActiveController();
        controller?.Simulate(client, this, GetActiveAnimator());
        StatusHandler.Tick();
        SimulateActiveChild(client, ActiveChild);
        InputHandler.CheckControls();
        Dragger?.UpdateTarget();
        StainHandler.TryGenerateStain();
    }

    /// <summary>
    ///     Called on respawn
    /// </summary>
    public override void Respawn()
    {
        SetModel(MODEL);

        LifeState = LifeState.Alive;

        Controller = new HumanController(this);
        Animator = new HumanAnimator();
        CameraMode = new FirstPersonCamera();

        EnableAllCollisions = true;
        EnableDrawing = true;
        EnableHideInFirstPerson = true;
        EnableShadowInFirstPerson = true;

        Inventory.Clear();

        RoleHandler.SpawnRole();

        SendTextNotification(To.Single(Client), "You are " + RoleHandler.Role.Name);

        base.Respawn();
    }

    public void Respawn(Vector3 position, Rotation rotation)
    {
        Respawn();

        Position = position;
        Rotation = rotation;
    }

    public void Respawn(SpawnPoint spawnPoint)
    {
        Respawn(spawnPoint.Position, spawnPoint.Rotation);
    }

    /// <summary>
    ///     Freeze the player in place
    ///     A player freezed can't do anything
    /// </summary>
    public void Freeze()
    {
        Log.Trace("[Player] Freeze");
        Controller.IsFrozen = true;
    }

    public void Unfreeze()
    {
        Log.Trace("[Player] Unfreeze");
        Controller.IsFrozen = false;
    }

    public override void OnKilled()
    {
        LifeState = LifeState.Dead;
        StopUsing();
        RagdollHandler.SpawnRagdoll(Vector3.Zero, 0);
        RoleHandler.Role?.OnKilled(this);
        Gamemode.Instance.RoundManager.CurrentRound.OnPlayerKilled(this);
    }

    public override void PostCameraSetup(ref CameraSetup setup)
    {
        base.PostCameraSetup(ref setup);
        Inventory.ViewModel?.PostCameraSetup(ref setup);
    }

    public void EnableSpectator()
    {
        Host.AssertServer();

        Controller = null;
        Animator = null;

        SpectatorCamera specCam = new();
        CameraMode = specCam;

        EnableAllCollisions = false;
        EnableDrawing = false;
    }

    [ClientRpc]
    private void SendTextNotification(string txt)
    {
        Log.Info("Trying to start event");
        Event.Run("ssl.notification", txt);
    }
}