using Sandbox;
using ssl.Player;

namespace ssl.Modules.Selection
{
    public class Selector
    {
        protected const float Range = 150f;

        protected readonly MainPlayer player;
        protected TraceResult traceResult;

        public Selector(MainPlayer player)
        {
            this.player = player;
        }

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

                Selected.OnSelect(player);
            }
            else if (Selected != null)
            {
                StopSelection();
            }
        }

        public void UseSelected()
        {
            Selected?.OnInteract(player);
        }

        private void StartSelection(ISelectable selectable)
        {
            Selected = selectable;
            Selected.OnSelectStart(player);
        }

        private void StopSelection()
        {
            Selected?.OnSelectStop(player);
            Selected = null;
        }


        protected virtual TraceResult GetTraceResult()
        {
            Vector3 forward = player.EyeRot.Forward;
            TraceResult tr = TraceSelector(player.EyePos, player.EyePos + forward * Range);
            return tr;
        }

        protected virtual TraceResult TraceSelector(Vector3 start, Vector3 end)
        {
            bool inWater = Physics.TestPointContents(start, CollisionLayer.Water);

            TraceResult tr = Trace.Ray(start, end)
                .UseHitboxes()
                .HitLayer(CollisionLayer.Water, !inWater)
                .Ignore(player)
                .Run();

            return tr;
        }
    }
}