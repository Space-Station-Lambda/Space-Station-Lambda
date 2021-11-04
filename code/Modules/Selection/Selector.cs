using Sandbox;
using ssl.Player;

namespace ssl.Modules.Selection
{
    public class Selector
    {
        protected const float Range = 150f;

        protected readonly Player.SslPlayer SslPlayer;
        protected TraceResult traceResult;

        public Selector(Player.SslPlayer sslPlayer)
        {
            this.SslPlayer = sslPlayer;
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

                Selected.OnSelect(SslPlayer);
            }
            else if (Selected != null)
            {
                StopSelection();
            }
        }

        public void UseSelected()
        {
            Selected?.OnInteract(SslPlayer);
        }

        private void StartSelection(ISelectable selectable)
        {
            Selected = selectable;
            Selected.OnSelectStart(SslPlayer);
        }

        private void StopSelection()
        {
            Selected?.OnSelectStop(SslPlayer);
            Selected = null;
        }


        protected virtual TraceResult GetTraceResult()
        {
            Vector3 forward = SslPlayer.EyeRot.Forward;
            TraceResult tr = TraceSelector(SslPlayer.EyePos, SslPlayer.EyePos + forward * Range);
            return tr;
        }

        protected virtual TraceResult TraceSelector(Vector3 start, Vector3 end)
        {
            bool inWater = Physics.TestPointContents(start, CollisionLayer.Water);

            TraceResult tr = Trace.Ray(start, end)
                .UseHitboxes()
                .HitLayer(CollisionLayer.Water, !inWater)
                .Ignore(SslPlayer)
                .Run();

            return tr;
        }
    }
}