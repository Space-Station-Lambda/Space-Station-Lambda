using System;
using ssl.Item;
using ssl.Item.ItemTypes;
using Xunit;

namespace ssl.Tests.Item
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
            ItemCore item = new ItemBase("test_food", "Test food", "");
            items.Add(item);
            Assert.True(items.Contains(item.Id));
            Assert.True(items.Contains(item));
        }

        [Fact]
        private void Should_Add_Item_Exist_Throw_Error()
        {
            ItemCore item = new ItemBase("test_food", "Test food", "");
            items.Add(item);
            Assert.Throws<Exception>(() => { items.Add(item); });
        }

        [Fact]
        private void Should_Get_Item()
        {
            ItemCore item = new ItemBase("test_food", "Test food", "");
            items.Add(item);
            ItemCore getItem = items.Get("test_food");
            Assert.Equal(item, getItem);
        }
    }
}