using System;
using Moq;
using ssl.Item;
using ssl.Item.ItemTypes;
using Xunit;

namespace ssl.Tests.Item
{
    public class ItemStackTests
    {
        private Mock<ItemCore> item;

        public ItemStackTests()
        {
            item = new Mock<ItemCore>("test", "Test Item", false)
            {
                CallBase = true
            };
        }

        [Fact]
        private void Should_Returns_New_ItemStack()
        {
            ItemStack itemStack = new(item.Object, 10);
            ItemStack newItemStack = itemStack.Remove(6);
            Assert.Equal(6, newItemStack.Amount);
            Assert.Equal(4, itemStack.Amount);
        }

        [Fact]
        private void Should_Returns_Same_ItemStack()
        {
            ItemStack itemStack = new(item.Object, 10);
            ItemStack newItemStack = itemStack.Remove(10);
            Assert.Equal(itemStack, newItemStack);
            Assert.Equal(10, newItemStack.Amount);
            Assert.Equal(10, itemStack.Amount);
        }


        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        private void Should_Throws_An_Exception_After_Stack_Reach(int amount)
        {
            ItemStack itemStack = new(item.Object, 10);

            Assert.Throws<Exception>(() => { itemStack.Add(amount); });
        }

        [Theory]
        [InlineData(11)]
        [InlineData(500)]
        private void Should_Throws_An_Exception_After_Stack_Negative(int amount)
        {
            //Arrange
            ItemStack itemStack = new(item.Object, 10);
            Assert.Throws<Exception>(() => { itemStack.Remove(amount); });
        }

        [Fact]
        private void ToString_Should_Return_Item_And_Amount()
        {
            ItemStack itemStack = new(item.Object);

            Assert.Equal($"{item.Object} (1)", itemStack.ToString());
        }
    }
}