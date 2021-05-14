using System.Collections.Generic;
using SSL_Core.interfaces;
using SSL_Core.player;
using SSL_Core.status;
using Xunit;

namespace SSL_Core_Tests.status
{
    public class StatusHandlerTests
    {
        [Fact]
        private void Test_Should_Add_Status()
        {
            StatusHandler<Player> statusHandler = new StatusHandler<Player>();

            Status<Player> status = new Status<Player>(1f, new List<IEffect<Player>>());
            
            statusHandler.AddStatus(status);

            Assert.Equal(1, statusHandler.StatusCount);
        }

        [Fact]
        private void Test_Should_Remove_Status()
        {
            StatusHandler<Player> statusHandler = new StatusHandler<Player>();

            Status<Player> status = new Status<Player>(1f, new List<IEffect<Player>>());
            
            statusHandler.AddStatus(status);
            
            statusHandler.RemoveStatus(status);
            Assert.Equal(0, statusHandler.StatusCount);
        }

        [Fact]
        private void Test_Should_Remove_On_Finished()
        {
            StatusHandler<Player> statusHandler = new StatusHandler<Player>();

            Status<Player> status = new Status<Player>(5f, new List<IEffect<Player>>());

            Player player = new Player();
            
            statusHandler.AddStatus(status);

            while (status.SecondsLeft > 0)
            {
                status.Update(player, 1f);
            }
            
            Assert.Equal(0, statusHandler.StatusCount);
        }

        [Fact]
        private void Test_Should_Update_Status()
        {
            StatusHandler<Player> statusHandler = new StatusHandler<Player>();

            Status<Player> status = new Status<Player>(5f, new List<IEffect<Player>>());
            Status<Player> status1 = new Status<Player>(10f, new List<IEffect<Player>>());

            Player player = new Player();
            
            statusHandler.AddStatus(status);
            statusHandler.AddStatus(status1);

            while (status.SecondsLeft > 0)
            {
                statusHandler.Update(player);
            }
            
            Assert.Equal(1, statusHandler.StatusCount);
            
            while (status1.SecondsLeft > 0)
            {
                statusHandler.Update(player);
            }
            
            Assert.Equal(0, statusHandler.StatusCount);

        }
        
    }
}