using Sandbox;
using ssl.Player;

namespace ssl.Modules.Selection
{
    public class Dragger : Selector
    {
        public Dragger(MainPlayer player) : base(player)
        {
        }
        
        public IDraggable Dragged { get; private set; }

        public override void UpdateTarget()
        {
            base.UpdateTarget();
            
            Entity result = GetTraceResultEntity();
            if (result is IDraggable draggable)
            {
                if (!ReferenceEquals(draggable, Dragged))
                {
                    StopDrag();
                    StartDrag(draggable);
                }

                Dragged.OnDrag(player);
            }
            else if (Selected != null)
            {
                StopDrag();
            }
        }

        private void StartDrag(IDraggable draggable)
        {
            //TODO: Create joint
        }

        private void StopDrag()
        {
            //TODO: Clear joint
        }
    }
}