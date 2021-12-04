using Sandbox;
using ssl.Modules.Elements.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Elements.Items.Carriables
{
    public partial class ItemFlashlight : Item
    {
        private const int Range = 512;
        private const float Falloff = 1.0f;
        private const float LinearAttenuation = 0.0f;
        private const float QuadraticAttenuation = 1.0f;
        private const int Brightness = 2;
        private const int InnerConeAngle = 20;
        private const int OuterConeAngle = 40;
        private const float FogStength = 1.0f;
        private const string SlideAttachementName = "slide";
        private const string LightAttachementName = "light";
        private const string FlashLightSoundSwitchOnName = "flashlight-on";
        private const string FlashLightSoundSwitchOffName = "flashlight-off";

        private TimeSince timeSinceLightToggled;
        private SpotLightEntity viewLight;
        private SpotLightEntity worldLight;

        private static Vector3 LightOffset => Vector3.Forward * 10;
        [Net, Local, Predicted] private bool LightEnabled { get; set; }

        public override void FrameSimulate(Client cl)
        {
            base.FrameSimulate(cl);
            if (!IsLocalPawn || !viewLight.IsValid()) return;
            viewLight.Rotation = Local.Pawn.EyeRot;
            viewLight.Position = Local.Pawn.EyePos + Local.Pawn.EyeRot.Forward * 10f;
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
                Range = Range,
                Falloff = Falloff,
                LinearAttenuation = LinearAttenuation,
                QuadraticAttenuation = QuadraticAttenuation,
                Brightness = Brightness,
                Color = Color.White,
                InnerConeAngle = InnerConeAngle,
                OuterConeAngle = OuterConeAngle,
                FogStength = FogStength,
                Owner = Owner,
            };

            light.UseFog();

            return light;
        }

        public override void OnUsePrimary(SslPlayer sslPlayer, ISelectable target)
        {
            base.OnUsePrimary(sslPlayer, target);
            
            if (timeSinceLightToggled > 0.1f)
            {
                
                LightEnabled = !LightEnabled;

                PlaySound(LightEnabled ? FlashLightSoundSwitchOnName : FlashLightSoundSwitchOffName);

                if (worldLight.IsValid())
                {
                    worldLight.Enabled = LightEnabled;
                }

                if (IsLocalPawn)
                {
                    if (!viewLight.IsValid())
                    {
                        CreateViewLight();
                    }
                    viewLight.Enabled = LightEnabled;
                }
                
                timeSinceLightToggled = 0;
            }
            
        }

        private void Activate()
        {
            if (worldLight.IsValid())
            {
                worldLight.Enabled = LightEnabled;
            }
        }

        private void Deactivate()
        {
            if (worldLight.IsValid())
            {
                worldLight.Enabled = false;
            }
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
            if (!viewLight.IsValid())
            {
                CreateViewLight();
            }
            viewLight.Enabled = state;
        }
    }
}
