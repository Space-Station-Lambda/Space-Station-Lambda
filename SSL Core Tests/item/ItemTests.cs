using Moq;
using SSL_Core.item.items;
using Xunit;

namespace SSL_Core_Tests.item
{
    public class ItemTests
    {
        [Fact]
        private void ToString_Should_Returns_Id_And_Name()
        {
            Mock<Item> mock = new Mock<Item>("test", "Test Item", false)
            {
                CallBase = true
            };

            Assert.Equal("[test] Test Item", mock.Object.ToString());
        }
    }
}