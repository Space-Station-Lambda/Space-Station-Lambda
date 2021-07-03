using System.Collections.Generic;
using SSL.Interfaces;
using SSL.Status;
using Xunit;

namespace SSL.Tests.Status
{
    public class StatusHandlerTests
    {
        [Fact]
        private void Should_Add_Status()
        {
            StatusHandler<Player.Player> statusHandler = new StatusHandler<Player.Player>();
            Status<Player.Player> status = new Status<Player.Player>(1f, new List<IEffect<Player.Player>>());
            
            statusHandler.AddStatus(status);

            Assert.Equal(1, statusHandler.StatusCount);
        }

        [Fact]
        private void Should_Remove_Status()
        {
            StatusHandler<Player.Player> statusHandler = new StatusHandler<Player.Player>();
            Status<Player.Player> status = new Status<Player.Player>(1f, new List<IEffect<Player.Player>>());
            
            statusHandler.AddStatus(status);
            
            statusHandler.RemoveStatus(status);
            Assert.Equal(0, statusHandler.StatusCount);
        }

        [Fact]
        private void Should_Remove_On_Finished()
        {
            Status<Player.Player> status = new Status<Player.Player>(5f, new List<IEffect<Player.Player>>());
            Player.Player player = new Player.Player();
            
            StatusHandler<Player.Player> statusHandler = new StatusHandler<Player.Player>();
            statusHandler.AddStatus(status);

            while (status.MillisLeft > 0)
            {
                status.Update(player, 1f);
            }
            
            Assert.Equal(0, statusHandler.StatusCount);
        }

        [Fact]
        private void Should_Update_Status()
        {
            Status<Player.Player> status = new Status<Player.Player>(5f, new List<IEffect<Player.Player>>());
            Status<Player.Player> status1 = new Status<Player.Player>(10f, new List<IEffect<Player.Player>>());
            Player.Player player = new Player.Player();
            
            StatusHandler<Player.Player> statusHandler = new StatusHandler<Player.Player>();

            statusHandler.AddStatus(status);
            statusHandler.AddStatus(status1);

            while (status.MillisLeft > 0)
            {
                statusHandler.Update(player);
            }
            
            Assert.Equal(1, statusHandler.StatusCount);
            
            while (status1.MillisLeft > 0)
            {
                statusHandler.Update(player);
            }
            
            Assert.Equal(0, statusHandler.StatusCount);
        }
    }
}
