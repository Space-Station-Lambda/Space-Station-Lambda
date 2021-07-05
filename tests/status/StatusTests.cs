using System.Collections.Generic;
using Moq;
using ssl.Interfaces;
using ssl.Player;
using ssl.Status;
using Xunit;

namespace ssl.Tests.Status
{
    public class StatusTests
    {
        private class TestEffect : IEffect<MainPlayer>
        {
            public int Counter = 0;

            public void Trigger(MainPlayer affected)
            {
                Counter++;
            }
        }

        [Theory]
        [InlineData(1000f, 100f)]
        [InlineData(985.4894f, 1894.14f)]
        private void Update_Correct_Time(float total, float step)
        {
            Mock<MainPlayer> player = new();
            Status<MainPlayer> status = new(total, new List<IEffect<MainPlayer>>());

            Assert.Equal(total, status.TotalMillis);
            Assert.Equal(total, status.MillisLeft);
            Assert.Equal(0f, status.MillisElapsed);

            status.Update(player.Object, step);

            Assert.Equal(total, status.TotalMillis);
            Assert.Equal(total - step, status.MillisLeft);
            Assert.Equal(step, status.MillisElapsed);

            int i;
            for (i = 0; i < (total - step) / step; i++) status.Update(player.Object, step);

            Assert.Equal(total, status.TotalMillis);
            Assert.True(status.MillisLeft <= 0f);
            Assert.Equal((i + 1) * step, status.MillisElapsed);
        }

        [Theory]
        [InlineData(1000f, 100f)]
        [InlineData(985.4894f, 1894.14f)]
        private void Finish_Event_Should_Trigger(float total, float step)
        {
            bool finished = false;
            Mock<MainPlayer> player = new();
            Status<MainPlayer> status = new(total, new List<IEffect<MainPlayer>>());

            status.StatusFinished += (s, elapsed) => finished = true;

            while (status.MillisLeft > 0) status.Update(player.Object, step);

            Assert.True(finished);
        }


        [Fact]
        private void Effects_Should_Apply()
        {
            TestEffect test = new();
            List<IEffect<MainPlayer>> effects = new() {test};
            Status<MainPlayer> status = new(10f, effects);
            Mock<MainPlayer> player = new();

            status.Update(player.Object);

            Assert.Equal(1, test.Counter);
        }
    }
}