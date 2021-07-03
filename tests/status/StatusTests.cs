using System.Collections.Generic;
using SSL.Interfaces;
using SSL.Status;
using Xunit;

namespace SSL.Tests.Status
{
    public class StatusTests
    {
        private class TestEffect : IEffect<Player.Player>
        {
            public int Counter = 0;
            
            public void Trigger(Player.Player affected)
            {
                Counter++;
            }
        }

        [Theory]
        [InlineData(1000f, 100f)]
        [InlineData(985.4894f, 1894.14f)]
        private void Update_Correct_Time(float total, float step)
        {
            Player.Player player = new Player.Player();
            Status<Player.Player> status = new Status<Player.Player>(total, new List<IEffect<Player.Player>>());
            
            Assert.Equal(total, status.TotalMillis);
            Assert.Equal(total, status.MillisLeft);
            Assert.Equal(0f, status.MillisElapsed);

            status.Update(player, step);
            
            Assert.Equal(total, status.TotalMillis);
            Assert.Equal(total - step, status.MillisLeft);
            Assert.Equal(step, status.MillisElapsed);
            
            int i;
            for (i=0;i<(total-step)/step;i++)
            {
                status.Update(player, step);
            }
            
            Assert.Equal(total, status.TotalMillis);
            Assert.True(status.MillisLeft <= 0f);
            Assert.Equal((i+1) * step, status.MillisElapsed);
        }

        [Theory]
        [InlineData(1000f, 100f)]
        [InlineData(985.4894f, 1894.14f)]
        private void Finish_Event_Should_Trigger(float total, float step)
        {
            bool finished = false;
            
            Player.Player player = new Player.Player();
            Status<Player.Player> status = new Status<Player.Player>(total, new List<IEffect<Player.Player>>());

            status.StatusFinished += (s, elapsed) => finished = true;
            
            while (status.MillisLeft > 0)
            {
                status.Update(player, step);
            }
            
            Assert.True(finished);
        }


        [Fact]
        private void Effects_Should_Apply()
        {
            TestEffect test = new TestEffect();
            
            List<IEffect<Player.Player>> effects = new List<IEffect<Player.Player>>();
            effects.Add(test);
            
            Status<Player.Player> status = new Status<Player.Player>(10f, effects);
            Player.Player player = new Player.Player();
            
            status.Update(player);
            
            Assert.Equal(1, test.Counter);
        }
        
    }
}
