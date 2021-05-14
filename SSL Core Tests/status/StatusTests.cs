using System.Collections.Generic;
using SSL_Core.interfaces;
using SSL_Core.player;
using SSL_Core.status;
using Xunit;

namespace SSL_Core_Tests.status
{
    public class StatusTests
    {
        [Theory]
        [InlineData(1000f, 100f)]
        [InlineData(985.4894f, 1894.14f)]
        private void Test_Update_Correct_Time(float total, float step)
        {
            Player player = new Player();
            Status<Player> status = new Status<Player>(total, new List<IEffect<Player>>());
            
            Assert.Equal(total, status.TotalSeconds);
            Assert.Equal(total, status.SecondsLeft);
            Assert.Equal(0f, status.SecondsElapsed);

            status.Update(player, step);
            
            Assert.Equal(total, status.TotalSeconds);
            Assert.Equal(total - step, status.SecondsLeft);
            Assert.Equal(step, status.SecondsElapsed);
            
            int i;
            for (i=0;i<(total-step)/step;i++)
            {
                status.Update(player, step);
            }
            
            Assert.Equal(total, status.TotalSeconds);
            Assert.True(status.SecondsLeft <= 0f);
            Assert.Equal((i+1) * step, status.SecondsElapsed);
        }

        [Theory]
        [InlineData(1000f, 100f)]
        [InlineData(985.4894f, 1894.14f)]
        private void Test_Finish_Event_Should_Trigger(float total, float step)
        {
            bool finished = false;
            
            Player player = new Player();
            Status<Player> status = new Status<Player>(total, new List<IEffect<Player>>());

            status.StatusFinished += (s, elapsed) => finished = true;
            
            while (status.SecondsLeft > 0)
            {
                status.Update(player, step);
            }
            
            Assert.True(finished);
        }
        
    }
}