﻿using Sandbox;

namespace ssl.Player.Cameras
{
    /// <summary>
    /// Camera that can be attached to an entity's attachment.
    /// </summary>
    public partial class AttachedCamera : Camera
    {
        private Vector3 lastPos;

        public AttachedCamera()
        {
        }
        
        public AttachedCamera(ModelEntity target, string attachment, Rotation offset, Vector3 startPos)
        {
            Target = target;
            Attachment = attachment;
            RotationOffset = offset;
            lastPos = startPos;
        }
        
        [Net] public ModelEntity Target { get; private set; }
        [Net] public string Attachment { get; private set; }
        [Net] public Rotation RotationOffset { get; private set; }

        public override void Activated()
        {
            base.Activated();
            ZNear = 0.01f;
        }

        public override void Update()
        {
            if (!Target.IsValid()) return;
            
            Transform targetTransform = Target.GetAttachment(Attachment).GetValueOrDefault(Target.Transform);
            
            if (targetTransform.Position.Distance(lastPos) < 300)
            {
                Pos = Vector3.Lerp(lastPos, targetTransform.Position, 20.0f * Time.Delta);
            }
            else
            {
                Pos = targetTransform.Position;
            }

            Rot = targetTransform.Rotation + targetTransform.RotationToWorld(RotationOffset);

            Viewer = Target;
            lastPos = Pos;
        }
    }
}