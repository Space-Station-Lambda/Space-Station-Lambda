using Sandbox;

namespace ssl.Player
{
    public interface ISelector
    {
        public void StopSelection();
        public void StartSelection(ISelectable selectable);
        public void CheckSelection();
    }
}