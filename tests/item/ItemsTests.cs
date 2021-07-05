using System;
using SSL.Item;
using SSL.Item.items;
using Xunit;

namespace SSL.Tests.Item
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
            SSL.Item.items.Item item = new ItemBase("test_food", "Test food");
            items.Add(item);
            Assert.True(items.Contains(item.Id));
            Assert.True(items.Contains(item));
        }
        
        [Fact]
        private void Should_Add_Item_Exist_Throw_Error()
        {
            SSL.Item.items.Item item = new ItemBase("test_food", "Test food");
            items.Add(item);
            Assert.Throws<Exception>(() =>
            {
                items.Add(item);
            });
        }
        
        [Fact]
        private void Should_Get_Item()
        {
            SSL.Item.items.Item item = new ItemBase("test_food", "Test food");
            items.Add(item);
            SSL.Item.items.Item getItem = items.Get("test_food");
            Assert.Equal(item, getItem);
        }
    }
}
