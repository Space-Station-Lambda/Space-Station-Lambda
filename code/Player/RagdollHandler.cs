using Sandbox;
using ssl.Player.Cameras;

namespace ssl.Player
{
    public class RagdollHandler
    {
        private const float DefaultTime = 6F;
            
        private MainPlayer player;
        
        public RagdollHandler(MainPlayer player)
        {
            this.player = player;
            TimeExitRagdoll = Time.Now;
        }
        
        public bool IsRagdoll => Ragdoll != null;
        public bool CanStand => ((TimeSince)TimeExitRagdoll).Absolute >= 0;
        private PlayerCorpse Ragdoll { get; set; }
        public float TimeExitRagdoll { get; set; }
        
        /// <summary>
        /// Activates the ragdoll mode of the player.
        /// </summary>
        public void StartRagdoll(float downTime = DefaultTime)
        {
            Ragdoll ??= SpawnRagdoll(player.Velocity, -1);
            player.SetParent(Ragdoll);
            player.EnableAllCollisions = false;
            player.EnableShadowInFirstPerson = false;
            player.Camera = new AttachedCamera(Ragdoll, "eyes", Rotation.From(-90, 90, 180), player.EyePos);

            TimeExitRagdoll = Time.Now + downTime;
        }

        /// <summary>
        /// Stop the ragdoll mode of the player.
        /// </summary>
        public void StopRagdoll()
        {
            if (!Ragdoll.IsValid) return;
            
            player.Position = Ragdoll.Position;
            player.Velocity = Vector3.Zero;
            
            player.SetParent(null);
            
            Ragdoll.Delete();
            Ragdoll = null;
            
            player.EnableAllCollisions = true;
            player.EnableShadowInFirstPerson = true;
            
            player.Camera = new FirstPersonCamera();
        }

        /// <summary>
        /// Spawns a ragdoll looking like the player.
        /// </summary>
        public PlayerCorpse SpawnRagdoll(Vector3 force, int forceBone)
        {
            PlayerCorpse ragdoll = new(player)
            {
                Position = player.Position,
                Rotation = player.Rotation
            };

            ragdoll.CopyFrom(player);
            ragdoll.ApplyForceToBone(force, forceBone);

            return ragdoll;
        }

        [ServerCmd("ragdoll")]
        private static void SetRagdoll(bool state)
        {
            MainPlayer player = (MainPlayer)ConsoleSystem.Caller.Pawn;
            if (state)
            {
                player.RagdollHandler.StartRagdoll();
            }
            else
            {
                player.RagdollHandler.StopRagdoll();
            }
        }
    }
}