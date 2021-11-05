﻿using Sandbox;
using ssl.Modules.Elements.Items.Data;

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

        public ItemFlashlight()
        {
        }

        public ItemFlashlight(ItemData itemData) : base(itemData)
        {
        }

        private static Vector3 LightOffset => Vector3.Forward * 10;
        [Net, Local, Predicted] private bool LightEnabled { get; set; } = true;

        public override void Spawn()
        {
            base.Spawn();

            worldLight = CreateLight();
            worldLight.SetParent(this, SlideAttachementName, new Transform(LightOffset));
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

        public override void Simulate(Client cl)
        {
            if (cl == null) return;

            base.Simulate(cl);

            bool toggle = Input.Pressed(InputButton.Flashlight) || Input.Pressed(InputButton.Attack1);

            if (timeSinceLightToggled > 0.1f && toggle)
            {
                LightEnabled = !LightEnabled;

                PlaySound(LightEnabled ? FlashLightSoundSwitchOnName : FlashLightSoundSwitchOffName);

                if (worldLight.IsValid())
                {
                    worldLight.Enabled = LightEnabled;
                }

                if (viewLight.IsValid())
                {
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
                }
                else
                {
                    Deactivate();
                }
            }
        }
    }
}