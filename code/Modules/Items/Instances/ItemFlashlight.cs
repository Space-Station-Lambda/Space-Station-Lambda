using Sandbox;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Instances;

public partial class ItemFlashlight : Item
{
    private const int RANGE = 512;
    private const float FALLOFF = 1.0f;
    private const float LINEAR_ATTENUATION = 0.0f;
    private const float QUADRATIC_ATTENUATION = 1.0f;
    private const int BRIGHTNESS = 2;
    private const int INNER_CONE_ANGLE = 20;
    private const int OUTER_CONE_ANGLE = 40;
    private const float FOG_STENGTH = 1.0f;
    private const string SLIDE_ATTACHEMENT_NAME = "slide";
    private const string LIGHT_ATTACHEMENT_NAME = "light";
    private const string FLASH_LIGHT_SOUND_SWITCH_ON_NAME = "flashlight-on";
    private const string FLASH_LIGHT_SOUND_SWITCH_OFF_NAME = "flashlight-off";

    private TimeSince timeSinceLightToggled;
    private SpotLightEntity viewLight;
    private SpotLightEntity worldLight;

    private static Vector3 LightOffset => Vector3.Forward * 10;
    [Net, Local, Predicted]   private bool LightEnabled { get; set; }

    public override void FrameSimulate(Client cl)
    {
        base.FrameSimulate(cl);
        if (!IsLocalPawn || !viewLight.IsValid()) return;

        viewLight.Rotation = Local.Pawn.EyeRotation;
        viewLight.Position = Local.Pawn.EyePosition + Local.Pawn.EyeRotation.Forward * 10f;
    }

    public override void Spawn()
    {
        base.Spawn();

        worldLight = CreateLight();
        worldLight.SetParent(this, null, new Transform(LightOffset));
        worldLight.EnableHideInFirstPerson = true;
        worldLight.Enabled = false;
    }

    private SpotLightEntity CreateLight()
    {
        SpotLightEntity light = new()
        {
            Enabled = true,
            DynamicShadows = true,
            Range = RANGE,
            Falloff = FALLOFF,
            LinearAttenuation = LINEAR_ATTENUATION,
            QuadraticAttenuation = QUADRATIC_ATTENUATION,
            Brightness = BRIGHTNESS,
            Color = Color.White,
            InnerConeAngle = INNER_CONE_ANGLE,
            OuterConeAngle = OUTER_CONE_ANGLE,
            FogStength = FOG_STENGTH,
            Owner = Owner
        };

        light.UseFog();

        return light;
    }

    public override void OnDownUsePrimary(SslPlayer sslPlayer, ISelectable target)
    {
        base.OnDownUsePrimary(sslPlayer, target);

        if (timeSinceLightToggled > 0.1f)
        {
            LightEnabled = !LightEnabled;

            PlaySound(LightEnabled ? FLASH_LIGHT_SOUND_SWITCH_ON_NAME : FLASH_LIGHT_SOUND_SWITCH_OFF_NAME);

            if (worldLight.IsValid()) worldLight.Enabled = LightEnabled;

            if (IsLocalPawn)
            {
                if (!viewLight.IsValid()) CreateViewLight();

                viewLight.Enabled = LightEnabled;
            }

            timeSinceLightToggled = 0;
        }
    }

    private void Activate()
    {
        if (worldLight.IsValid()) worldLight.Enabled = LightEnabled;
    }

    private void Deactivate()
    {
        if (worldLight.IsValid()) worldLight.Enabled = false;
    }

    public override void ActiveStart(Entity ent)
    {
        base.ActiveStart(ent);

        if (Host.IsServer)
        {
            Activate();

            SetViewLight(To.Single(Owner), LightEnabled);
        }
    }

    public override void ActiveEnd(Entity ent, bool dropped)
    {
        base.ActiveEnd(ent, dropped);
        if (Host.IsServer)
        {
            if (dropped)
            {
                Activate();
                DestroyViewLight(To.Single(Owner));
            }
            else
            {
                Deactivate();
            }

            SetViewLight(To.Single(Owner), false);
        }
    }

    private void CreateViewLight()
    {
        Host.AssertClient();
        viewLight ??= CreateLight();
        viewLight.SetParent(Local.Pawn, false);
        viewLight.EnableViewmodelRendering = true;
        viewLight.Enabled = false;
    }

    [ClientRpc]
    private void DestroyViewLight()
    {
        viewLight.Delete();
        viewLight = null;
    }

    [ClientRpc]
    private void SetViewLight(bool state)
    {
        if (!viewLight.IsValid()) CreateViewLight();

        viewLight.Enabled = state;
    }
}