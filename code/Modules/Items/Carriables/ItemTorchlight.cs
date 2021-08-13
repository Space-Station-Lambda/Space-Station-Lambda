using Sandbox;
using ssl.Modules.Items.Data;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemTorchlight : Item
    {
        
        private SpotLightEntity worldLight;
        private SpotLightEntity viewLight;
        public ItemTorchlight()
        {
        }
        public ItemTorchlight(ItemData data) : base(data)
        {
        }
        
        public override string ViewModelPath => "weapons/rust_flashlight/v_rust_flashlight.vmdl";
        protected Vector3 LightOffset => Vector3.Forward * 10;
        
        [Net, Local, Predicted]
        private bool LightEnabled { get; set; } = true;
        
        TimeSince timeSinceLightToggled;
        
        public override void Spawn()
        {
            base.Spawn();

            SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );

            worldLight = CreateLight();
            worldLight.SetParent( this, "slide", new Transform( LightOffset ) );
            worldLight.EnableHideInFirstPerson = true;
            worldLight.Enabled = false;
        }
        
        public override void CreateViewModel()
        {
            base.CreateViewModel();

            viewLight = CreateLight();
            viewLight.SetParent( ViewModelEntity, "light", new Transform( LightOffset ) );
            viewLight.EnableViewmodelRendering = true;
            viewLight.Enabled = LightEnabled;
        }
        
        private SpotLightEntity CreateLight()
        {
            SpotLightEntity light = new()
            {
                Enabled = true,
                DynamicShadows = true,
                Range = 512,
                Falloff = 1.0f,
                LinearAttenuation = 0.0f,
                QuadraticAttenuation = 1.0f,
                Brightness = 2,
                Color = Color.White,
                InnerConeAngle = 20,
                OuterConeAngle = 40,
                FogStength = 1.0f,
                Owner = Owner,
            };

            light.UseFog();

            return light;
        }
        
        public override void Simulate( Client cl )
        {
            if ( cl == null )
                return;

            base.Simulate( cl );

            bool toggle = Input.Pressed( InputButton.Flashlight ) || Input.Pressed( InputButton.Attack1 );

            if ( timeSinceLightToggled > 0.1f && toggle )
            {
                LightEnabled = !LightEnabled;

                PlaySound( LightEnabled ? "flashlight-on" : "flashlight-off" );

                if ( worldLight.IsValid() )
                {
                    worldLight.Enabled = LightEnabled;
                }

                if ( viewLight.IsValid() )
                {
                    viewLight.Enabled = LightEnabled;
                }

                timeSinceLightToggled = 0;
            }
            
        }
        private void Activate()
        {
            if ( worldLight.IsValid() )
            {
                worldLight.Enabled = LightEnabled;
            }
        }
        
        private void Deactivate()
        {
            if ( worldLight.IsValid() )
            {
                worldLight.Enabled = false;
            }
        }
        
        public override void ActiveStart( Entity ent )
        {
            base.ActiveStart( ent );

            if ( IsServer )
            {
                Activate();
            }
        }

        public override void ActiveEnd( Entity ent, bool dropped )
        {
            base.ActiveEnd( ent, dropped );

            if ( IsServer )
            {
                if ( dropped )
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