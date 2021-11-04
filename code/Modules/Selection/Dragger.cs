using Sandbox;
using Sandbox.Joints;
using ssl.Player;

namespace ssl.Modules.Selection
{
    public class Dragger : Selector
    {
        private const float HoldDistance = 50F;
        private const float LinearFrequency = 10F;
        private const float LinearDampingRatio = 1F;
        private const float AngularFrequency = 10F;
        private const float AngularDampingRatio = 1F;
        private const float BreakLinearForce = 2000F;
        
        private PhysicsBody holdBody;
        private WeldJoint holdJoint;
        
        public Dragger(Player.SslPlayer sslPlayer) : base(sslPlayer)
        {
            Activate();
        }

        public IDraggable Dragged { get; private set; }
        public PhysicsBody HeldBody { get; private set; }
        public Rotation HeldRot { get; private set; }

        /// <summary>
        /// Updates the Dragger system.
        /// Will start dragging item if there's not one already or will just update the position of the "hand".
        /// </summary>
        public void Drag()
        {
            if (Dragged == null)
            {
                //Try to start dragging the entity currently targeted.
                if (IsSelectedDraggable()) TryDrag();
            }
            else
            {
                // Updates the position of the "hand" of dragger. 
                GrabMove(SslPlayer.EyePos, SslPlayer.EyeRot.Forward, SslPlayer.EyeRot);
            }
        }

        /// <summary>
        /// Start dragging an Entity if it is a draggable.
        /// Will stop dragging the previous one if there's any.
        /// </summary>
        /// <param name="grabPos">Destination of the dragged entity. (Relative to World)</param>
        /// <param name="grabRot">Ideal Rotation of the dragged entity. (Relative to World)</param>
        private void StartDrag(Vector3 grabPos, Rotation grabRot)
        {
            if (!IsSelectedDraggable()) 
                return;
            
            StopDrag();
            
            Dragged = (IDraggable)Selected;
            
            HeldBody = traceResult.Body;
            HeldBody.Wake();
            HeldBody.EnableAutoSleeping = false;
            
            HeldRot = grabRot.Inverse * HeldBody.Rotation;

            holdBody.Position = grabPos;
            holdBody.Rotation = HeldBody.Rotation;
            
            holdJoint = PhysicsJoint.Weld
                .From(holdBody)
                .To(HeldBody, HeldBody.LocalMassCenter)
                .WithLinearSpring(LinearFrequency, LinearDampingRatio, 0.0f)
                .WithAngularSpring(AngularFrequency, AngularDampingRatio, 0.0f)
                .Breakable(HeldBody.Mass * BreakLinearForce, 0)
                .Create();

            Dragged.OnDragStart(SslPlayer);
        }

        /// <summary>
        /// Releases the Draggable currently being dragged, if there's one.
        /// </summary>
        public void StopDrag()
        {
            if (holdJoint.IsValid)
            {
                holdJoint.Remove();
            }

            if (Dragged != null)
            {
                HeldBody.EnableAutoSleeping = true;
                Dragged.OnDragStop(SslPlayer);
            }
            
            HeldBody = null;
            Dragged = null;
            HeldRot = Rotation.Identity;
        }

        private bool IsSelectedDraggable()
        {
            if (Selected is not IDraggable draggable)
                return false;

            if (!traceResult.Body.IsValid())
                return false;

            if (traceResult.Body.PhysicsGroup == null)
                return false;
            
            return true;
        }

        private void TryDrag()
        {
            if (((IDraggable)Selected).IsDraggable(SslPlayer))
            {
                StartDrag(SslPlayer.EyePos + SslPlayer.EyeRot.Forward * HoldDistance, SslPlayer.EyeRot);
            }
        }

        private void GrabMove(Vector3 startPos, Vector3 dir, Rotation rot)
        {
            if (!HeldBody.IsValid())
                return;

            Dragged.OnDrag(SslPlayer);
            
            holdBody.Position = startPos + dir * HoldDistance;
            holdBody.Rotation = rot * HeldRot;
        }

        private void Activate()
        {
            if (!holdBody.IsValid())
            {
                holdBody = new PhysicsBody
                {
                    BodyType = PhysicsBodyType.Keyframed
                };
            }
        }

        private void Deactivate()
        {
            holdBody?.Remove();
            holdBody = null;
        }
    }
}