using Moq;
using ssl.Item.ItemTypes;
using Xunit;

namespace ssl.Tests.Item
{
    public class ItemTests
    {
        [Fact]
        private void ToString_Should_Returns_Id_And_Name()
        {
            Mock<ItemCore> mock = new("test", "Test Item", "test", 1, false)
            {
                CallBase = true
            };

            Assert.Equal("[test] Test Item", mock.Object.ToString());
        }
    }
}