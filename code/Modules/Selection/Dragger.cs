using Sandbox;
using Sandbox.Joints;
using ssl.Player;

namespace ssl.Modules.Selection
{
    public class Dragger : Selector
    {
        private const float HoldDistance = 50F;
        private const float LinearFrequency = 10.0f;
        private const float LinearDampingRatio = 1.0f;
        private const float AngularFrequency = 10.0f;
        private const float AngularDampingRatio = 1.0f;
        private const float BreakLinearForce = 2000.0f;
        
        private PhysicsBody holdBody;
        private WeldJoint holdJoint;
        
        public Dragger(MainPlayer player) : base(player)
        {
            Activate();
        }

        public IDraggable Dragged { get; private set; }
        public PhysicsBody HeldBody => Dragged.Body;
        public Rotation HeldRot { get; private set; }
        public Entity HeldEntity { get; private set; }

        public void Drag()
        {
            if (null == Dragged)
            {
                Entity entity = GetTraceResultEntity();
                if (entity is not IDraggable draggable) return;
                if (draggable.IsDraggable(player))
                {
                    StartDrag(entity, player.EyePos + player.EyeRot.Forward * HoldDistance, player.EyeRot);
                }
            }
            else
            {
                GrabMove(player.EyePos, player.EyeRot.Forward, player.EyeRot);
            }
        }

        public void StopDrag()
        {
            if (holdJoint.IsValid)
            {
                holdJoint.Remove();
            }

            if (Dragged != null)
            {
                HeldBody.EnableAutoSleeping = true;
                Dragged.OnDragStop(player);
            }

            if (HeldEntity.IsValid())
            {
                Client client = player.GetClientOwner();
                client?.Pvs.Remove( HeldEntity );
            }

            Dragged = null;
            HeldRot = Rotation.Identity;
            HeldEntity = null;
        }

        private void StartDrag(Entity entity, Vector3 grabPos, Rotation grabRot)
        {
            if (entity is not IDraggable draggable)
                return;

            if (!draggable.Body.IsValid())
                return;

            if (draggable.Body.PhysicsGroup == null)
                return;

            if (null != Dragged)
                return;

            StopDrag();

            Dragged = draggable;
            
            HeldRot = grabRot.Inverse * HeldBody.Rotation;

            holdBody.Position = grabPos;
            holdBody.Rotation = HeldBody.Rotation;

            HeldBody.Wake();
            HeldBody.EnableAutoSleeping = false;
            
            holdJoint = PhysicsJoint.Weld
                .From(holdBody)
                .To(HeldBody, HeldBody.LocalMassCenter)
                .WithLinearSpring(LinearFrequency, LinearDampingRatio, 0.0f)
                .WithAngularSpring(AngularFrequency, AngularDampingRatio, 0.0f)
                .Breakable(HeldBody.Mass * BreakLinearForce, 0)
                .Create();

            HeldEntity = entity;
            Client client = player.Owner.GetClientOwner();
            client?.Pvs.Add( HeldEntity );
            
            draggable.OnDragStart(player);
        }

        private void GrabMove(Vector3 startPos, Vector3 dir, Rotation rot)
        {
            if (!HeldBody.IsValid())
                return;

            Dragged.OnDrag(player);
            
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