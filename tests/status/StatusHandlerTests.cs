using System.Collections.Generic;
using SSL.Interfaces;
using SSL.Player;
using SSL.Status;
using Xunit;

namespace SSL.Tests.Status
{
    public class StatusHandlerTests
    {
        [Fact]
        private void Should_Add_Status()
        {
            StatusHandler<MainPlayer> statusHandler = new();
            Status<MainPlayer> status = new Status<MainPlayer>(1f, new List<IEffect<MainPlayer>>());
            
            statusHandler.AddStatus(status);

            Assert.Equal(1, statusHandler.StatusCount);
        }

        [Fact]
        private void Should_Remove_Status()
        {
            StatusHandler<MainPlayer> statusHandler = new StatusHandler<MainPlayer>();
            Status<MainPlayer> status = new Status<MainPlayer>(1f, new List<IEffect<MainPlayer>>());
            
            statusHandler.AddStatus(status);
            
            statusHandler.RemoveStatus(status);
            Assert.Equal(0, statusHandler.StatusCount);
        }

        [Fact]
        private void Should_Remove_On_Finished()
        {
            Status<MainPlayer> status = new Status<MainPlayer>(5f, new List<IEffect<MainPlayer>>());
            MainPlayer player = new MainPlayer();
            
            StatusHandler<MainPlayer> statusHandler = new StatusHandler<MainPlayer>();
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
            Status<MainPlayer> status = new(5f, new List<IEffect<MainPlayer>>());
            Status<MainPlayer> status1 = new(10f, new List<IEffect<MainPlayer>>());
            MainPlayer player = new MainPlayer();
            
            StatusHandler<MainPlayer> statusHandler = new();

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
