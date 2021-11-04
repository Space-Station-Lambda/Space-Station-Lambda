using Sandbox;
using ssl.Player.Cameras;

namespace ssl.Player
{
    public class RagdollHandler
    {
        private const float DefaultTime = 6F;
            
        private SslPlayer sslPlayer;
        
        public RagdollHandler(SslPlayer sslPlayer)
        {
            this.sslPlayer = sslPlayer;
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
            Ragdoll ??= SpawnRagdoll(sslPlayer.Velocity, -1);
            sslPlayer.SetParent(Ragdoll);
            sslPlayer.EnableAllCollisions = false;
            sslPlayer.EnableShadowInFirstPerson = false;
            sslPlayer.Camera = new AttachedCamera(Ragdoll, "eyes", Rotation.From(-90, 90, 180), sslPlayer.EyePos);

            TimeExitRagdoll = Time.Now + downTime;
        }

        /// <summary>
        /// Stop the ragdoll mode of the player.
        /// </summary>
        public void StopRagdoll()
        {
            if (!Ragdoll.IsValid) return;
            
            sslPlayer.Position = Ragdoll.Position;
            sslPlayer.Velocity = Vector3.Zero;
            
            sslPlayer.SetParent(null);
            
            Ragdoll.Delete();
            Ragdoll = null;
            
            sslPlayer.EnableAllCollisions = true;
            sslPlayer.EnableShadowInFirstPerson = true;
            
            sslPlayer.Camera = new FirstPersonCamera();
        }

        /// <summary>
        /// Spawns a ragdoll looking like the player.
        /// </summary>
        public PlayerCorpse SpawnRagdoll(Vector3 force, int forceBone)
        {
            PlayerCorpse ragdoll = new(sslPlayer)
            {
                Position = sslPlayer.Position,
                Rotation = sslPlayer.Rotation
            };

            ragdoll.CopyFrom(sslPlayer);
            ragdoll.ApplyForceToBone(force, forceBone);

            return ragdoll;
        }

        [ServerCmd("ragdoll")]
        private static void SetRagdoll(bool state)
        {
            SslPlayer sslPlayer = (SslPlayer)ConsoleSystem.Caller.Pawn;
            if (state)
            {
                sslPlayer.RagdollHandler.StartRagdoll();
            }
            else
            {
                sslPlayer.RagdollHandler.StopRagdoll();
            }
        }
        
        [AdminCmd("spawn_ragdoll")]
        private static void SpawnRagdoll()
        {
            SslPlayer sslPlayer = (SslPlayer)ConsoleSystem.Caller.Pawn;
            sslPlayer.RagdollHandler.SpawnRagdoll(Vector3.Zero, -1);
        }
    }
}