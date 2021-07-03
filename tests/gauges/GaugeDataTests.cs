using SSL.Gauge;
using Xunit;

namespace SSL.Tests.Gauges
{
    public class GaugeDataTests
    {
        private readonly GaugeData gaugeData;

        public GaugeDataTests()
        {
            gaugeData = new GaugeData("TEST_GAUGEDATA", 0, 100);
        }
        
        [Fact]
        private void Test_ToString()
        { 
            Assert.Equal("[TEST_GAUGEDATA] 0|100", gaugeData.ToString());
        }
      
    }
}
