using SSL_Core.exception;
using SSL_Core.item;
using SSL_Core.item.items;
using Xunit;

namespace SSL_Core_Tests.item
{
    public class ItemsTests
    {
        private readonly Items items;

        public ItemsTests()
        {
            items = new Items();
        }

        [Fact]
        private void Should_Add_Item()
        {
            Item item = new ItemBase("test_food", "Test food");
            items.Add(item);
            Assert.True(items.Contains(item.Id));
            Assert.True(items.Contains(item));
        }
        
        [Fact]
        private void Should_Add_Item_Exist_Throw_Error()
        {
            Item item = new ItemBase("test_food", "Test food");
            items.Add(item);
            Assert.Throws<ItemAlreadyExistsException>(() =>
            {
                items.Add(item);
            });
        }
        
        [Fact]
        private void Should_Get_Item()
        {
            Item item = new ItemBase("test_food", "Test food");
            items.Add(item);
            Item getItem = items.Get("test_food");
            Assert.Equal(item, getItem);
        }
    }
}