using Sandbox;
using ssl.Player.Cameras;

namespace ssl.Player;

public class RagdollHandler : EntityComponent<SslPlayer>
{
    private const float DEFAULT_TIME = 6F;

    public RagdollHandler()
    {
        TimeExitRagdoll = Time.Now;
    }

    public bool IsRagdoll => Ragdoll != null;
    public bool CanStand => ((TimeSince) TimeExitRagdoll).Absolute >= 0;
    private PlayerCorpse Ragdoll { get; set; }
    public float TimeExitRagdoll { get; set; }

    /// <summary>
    ///     Activates the ragdoll mode of the player.
    /// </summary>
    public void StartRagdoll(float downTime = DEFAULT_TIME)
    {
        Ragdoll ??= SpawnRagdoll(Entity.Velocity, -1);
        Entity.SetParent(Ragdoll);
        Entity.EnableAllCollisions = false;
        Entity.EnableShadowInFirstPerson = false;
        Entity.Camera = new AttachedCamera(Ragdoll, "eyes", Rotation.From(-90, 90, 180), Entity.EyePos);
        Entity.EnableDrawing = false;

        TimeExitRagdoll = Time.Now + downTime;
    }

    /// <summary>
    ///     Stop the ragdoll mode of the player.
    /// </summary>
    public void StopRagdoll()
    {
        if (!Ragdoll.IsValid) return;

        Entity.Position = Ragdoll.Position;
        Entity.Velocity = Vector3.Zero;

        Entity.SetParent(null);

        Ragdoll.Delete();
        Ragdoll = null;

        Entity.EnableAllCollisions = true;
        Entity.EnableShadowInFirstPerson = true;
        Entity.EnableDrawing = true;
        Entity.Camera = new FirstPersonCamera();
    }

    /// <summary>
    ///     Spawns a ragdoll looking like the player.
    /// </summary>
    public PlayerCorpse SpawnRagdoll(Vector3 force, int forceBone)
    {
        PlayerCorpse ragdoll = new(Entity) { Position = Entity.Position, Rotation = Entity.Rotation };

        ragdoll.CopyFrom(Entity);
        ragdoll.ApplyForceToBone(force, forceBone);

        return ragdoll;
    }

    [ServerCmd("ragdoll")]
    private static void SetRagdoll(bool state)
    {
        SslPlayer sslPlayer = (SslPlayer) ConsoleSystem.Caller.Pawn;
        if (state)
            sslPlayer.RagdollHandler.StartRagdoll();
        else
            sslPlayer.RagdollHandler.StopRagdoll();
    }

    [AdminCmd("spawn_ragdoll")]
    private static void SpawnRagdoll()
    {
        SslPlayer sslPlayer = (SslPlayer) ConsoleSystem.Caller.Pawn;
        sslPlayer.RagdollHandler.SpawnRagdoll(Vector3.Zero, -1);
    }
}