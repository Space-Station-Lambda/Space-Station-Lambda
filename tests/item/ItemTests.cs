using Moq;
using Xunit;

namespace SSL.Tests.Item
{
    public class ItemTests
    {
        [Fact]
        private void ToString_Should_Returns_Id_And_Name()
        {
            Mock<SSL.Item.items.Item> mock = new Mock<SSL.Item.items.Item>("test", "Test Item", false)
            {
                CallBase = true
            };

            Assert.Equal("[test] Test Item", mock.Object.ToString());
        }
    }
}
