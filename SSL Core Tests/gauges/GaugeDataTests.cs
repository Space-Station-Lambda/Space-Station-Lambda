using SSL_Core.exception;
using SSL_Core.gauges;
using Xunit;

namespace SSL_Core_Tests.gauges
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
