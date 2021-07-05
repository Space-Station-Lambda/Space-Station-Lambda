using System.Collections.Generic;
using Moq;
using ssl.Interfaces;
using ssl.Player;
using ssl.Status;
using Xunit;

namespace ssl.Tests.Status
{
    public class StatusHandlerTests
    {
        [Fact]
        private void Should_Add_Status()
        {
            StatusHandler<MainPlayer> statusHandler = new();
            Status<MainPlayer> status = new(1f, new List<IEffect<MainPlayer>>());

            statusHandler.AddStatus(status);

            Assert.Equal(1, statusHandler.StatusCount);
        }

        [Fact]
        private void Should_Remove_Status()
        {
            StatusHandler<MainPlayer> statusHandler = new();
            Status<MainPlayer> status = new(1f, new List<IEffect<MainPlayer>>());

            statusHandler.AddStatus(status);

            statusHandler.RemoveStatus(status);
            Assert.Equal(0, statusHandler.StatusCount);
        }

        [Fact]
        private void Should_Remove_On_Finished()
        {
            Status<MainPlayer> status = new(5f, new List<IEffect<MainPlayer>>());
            Mock<MainPlayer> player = new();

            StatusHandler<MainPlayer> statusHandler = new();
            statusHandler.AddStatus(status);

            while (status.MillisLeft > 0) status.Update(player.Object, 1f);

            Assert.Equal(0, statusHandler.StatusCount);
        }

        [Fact]
        private void Should_Update_Status()
        {
            Status<MainPlayer> status = new(5f, new List<IEffect<MainPlayer>>());
            Status<MainPlayer> status1 = new(10f, new List<IEffect<MainPlayer>>());
            Mock<MainPlayer> player = new();

            StatusHandler<MainPlayer> statusHandler = new();

            statusHandler.AddStatus(status);
            statusHandler.AddStatus(status1);

            while (status.MillisLeft > 0) statusHandler.Update(player.Object);

            Assert.Equal(1, statusHandler.StatusCount);

            while (status1.MillisLeft > 0) statusHandler.Update(player.Object);

            Assert.Equal(0, statusHandler.StatusCount);
        }
    }
}