using SSL_Core.exception;
using SSL_Core.model.item;
using SSL_Core.model.item.items;
using Xunit;

namespace SSL_Core_Tests.model.item
{
    public class ItemsTests
    {
        private readonly Items items;

        public ItemsTests()
        {
            items = Items.Instance;
        }

        [Fact]
        private void ShouldAddItem()
        {
            Item item = new ItemBase("test_food", "Test food");
            items.Add(item);
            Assert.True(items.Contains(item.Id));
            Assert.True(items.Contains(item));
        }
        
        [Fact]
        private void ShouldAddItemExistThrowError()
        {
            Item item = new ItemBase("test_food", "Test food");
            items.Add(item);
            Assert.Throws<ItemAlreadyExistsException>(() =>
            {
                items.Add(item);
            });
        }
    }
}