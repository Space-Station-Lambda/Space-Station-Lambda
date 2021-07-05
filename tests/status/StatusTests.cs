using System.Collections.Generic;
using SSL.Interfaces;
using SSL.Player;
using SSL.Status;
using Xunit;

namespace SSL.Tests.Status
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
            MainPlayer player = new MainPlayer();
            Status<MainPlayer> status = new Status<MainPlayer>(total, new List<IEffect<MainPlayer>>());
            
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
            
            MainPlayer player = new MainPlayer();
            Status<MainPlayer> status = new Status<MainPlayer>(total, new List<IEffect<MainPlayer>>());

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
            
            List<IEffect<MainPlayer>> effects = new List<IEffect<MainPlayer>>();
            effects.Add(test);
            
            Status<MainPlayer> status = new Status<MainPlayer>(10f, effects);
            MainPlayer player = new MainPlayer();
            
            status.Update(player);
            
            Assert.Equal(1, test.Counter);
        }
        
    }
}
