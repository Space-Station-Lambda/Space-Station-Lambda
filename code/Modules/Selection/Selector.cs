using Sandbox;
using ssl.Player;

namespace ssl.Modules.Selection
{
    public class Selector : EntityComponent<SslPlayer>
    {
        protected const float Range = 150f;
        
        protected TraceResult traceResult;

        public ISelectable Selected { get; private set; }
        public bool IsSelecting => Selected != null;
        
        public virtual void UpdateTarget()
        {
            traceResult = GetTraceResult();
                
            if (traceResult.Entity is ISelectable selectable)
            {
                if (!ReferenceEquals(selectable, Selected))
                {
                    StopSelection();
                    StartSelection(selectable);
                }

                Selected.OnSelect(Entity);
            }
            else if (Selected != null)
            {
                StopSelection();
            }
        }

        public void UseSelected()
        {
            Selected?.OnInteract(Entity, 1);
        }

        private void StartSelection(ISelectable selectable)
        {
            Selected = selectable;
            Selected.OnSelectStart(Entity);
        }

        private void StopSelection()
        {
            Selected?.OnSelectStop(Entity);
            Selected = null;
        }


        protected virtual TraceResult GetTraceResult()
        {
            Vector3 forward = Entity.EyeRot.Forward;
            TraceResult tr = TraceSelector(Entity.EyePos, Entity.EyePos + forward * Range);
            return tr;
        }

        protected virtual TraceResult TraceSelector(Vector3 start, Vector3 end)
        {
            bool inWater = Physics.TestPointContents(start, CollisionLayer.Water);

            TraceResult tr = Trace.Ray(start, end)
                .UseHitboxes()
                .HitLayer(CollisionLayer.Water, !inWater)
                .Ignore(Entity)
                .Run();

            return tr;
        }
    }
}