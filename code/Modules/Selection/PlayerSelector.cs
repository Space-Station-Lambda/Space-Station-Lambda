using Sandbox;

namespace ssl.Player
{
    public class PlayerSelector : ISelector
    {
        private const float SelectionRange = 1000f;
        
        private MainPlayer player;
        private ISelectable selected;
        
        
        public PlayerSelector(MainPlayer player)
        {
            this.player = player;
        }

        public void CheckSelection()
        {
            Vector3 forward = player.EyeRot.Forward;
            TraceResult tr = TraceSelector(player.EyePos, player.EyePos + forward * SelectionRange);
            Entity result = tr.Entity;
            if (result is ISelectable selectable)
            {
                if (selected != selectable)
                {
                    StopSelection();
                    StartSelection(selectable);
                }
                selected.OnSelect(this);
            }
            else
            {
                StopSelection();
            }
                
        }

        public void StartSelection(ISelectable selectable)
        {
            selected.OnSelectStop(this);
            selected = null;
        }
        
        public void StopSelection()
        {
            selected.OnSelectStop(this);
            selected = null;
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