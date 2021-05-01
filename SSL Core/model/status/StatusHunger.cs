using SSL_Core.model.player;

namespace SSL_Core.model.status
{
    public class StatusHunger : Status<Player>
    {
        public StatusHunger(float seconds) : base(seconds)
        {
        }

        public override void Update(Player affected)
        {
            throw new System.NotImplementedException();
        }
    }
}