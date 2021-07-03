using Moq;
using Ssl.Item.items;
using Xunit;

namespace SSL_Tests.item
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
