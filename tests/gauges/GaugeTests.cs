using SSL.exception;
using SSL.gauges;
using Xunit;

namespace SSL_Tests.gauges
{
    public class GaugeTests
    {
        private readonly Gauge gauge;

        public GaugeTests()
        {
            gauge = new Gauge(new GaugeData("TEST_GAUGE", 0, 100));
        }
        
        [Theory]
        [InlineData(10)]
        [InlineData(90)]
        private void Should_Add_Value(int value)
        {
            gauge.AddValue(value);
            Assert.Equal(value, gauge.Value);
        }
        
        [Fact]
        private void Should_Throw_Exception()
        {
            Assert.Throws<OutOfGaugeException>(() =>
            {
                gauge.AddValue(500);
            });
        }
        
        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        private void Should_Have_Good_Value_Left(int value)
        {
            gauge.AddValue(value);
            Assert.Equal(gauge.GaugeData.MaxValue - value, gauge.ValueLeft);
        }

        [Fact]
        private void Test_ToString()
        { 
            gauge.AddValue(10);
            Assert.Equal("[TEST_GAUGE] 10/100", gauge.ToString());
        }
      
    }
}
