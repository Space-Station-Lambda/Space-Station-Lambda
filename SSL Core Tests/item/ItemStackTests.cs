using Moq;
using SSL_Core.exception;
using SSL_Core.item;
using SSL_Core.item.items;
using Xunit;

namespace SSL_Core_Tests.item
{
    public class ItemStackTests
    {
        [Fact]
        void Should_Returns_New_ItemStack()
        {
            Item item = new ItemBase("ITEM_BASE_TEST", "Test");
            ItemStack itemStack = new(item, 10);
            ItemStack newItemStack = itemStack.Remove(6);
            Assert.Equal(6, newItemStack.Amount);
            Assert.Equal(4, itemStack.Amount);
        }
        
        [Fact]
        void Should_Returns_Same_ItemStack()
        {
            Item item = new ItemBase("ITEM_BASE_TEST", "Test");
            ItemStack itemStack = new(item, 10);
            ItemStack newItemStack = itemStack.Remove(10);
            Assert.Equal(itemStack, newItemStack);
            Assert.Equal(10, newItemStack.Amount);
            Assert.Equal(10, itemStack.Amount);
        }

        
        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        void Should_Throws_An_Exception_After_Stack_Reach(int amount)
        {
            Item item = new ItemBase("ITEM_BASE_TEST", "Test");
            ItemStack itemStack = new(item, 10);

            Assert.Throws<OutOfStackItemStackException>(() =>
            {
                itemStack.Add(amount);
            });
        }
        
        [Theory]
        [InlineData(11)]
        [InlineData(500)]
        void Should_Throws_An_Exception_After_Stack_Negative(int amount)
        {
            //Arrange
            Item item = new ItemBase("ITEM_BASE_TEST", "Test", 100);
            ItemStack itemStack = new(item, 10);
            Assert.Throws<NegativeItemStackException>(() =>
            {
                itemStack.Remove(amount);
            });
        }

        [Fact]
        private void ToString_Should_Return_Item_And_Amount()
        {
            Mock<Item> mockItem = new Mock<Item>("test", "Test Item", false)
            {
                CallBase = true
            };
            ItemStack itemStack = new ItemStack(mockItem.Object);
            
            Assert.Equal($"{mockItem.Object} (1)", itemStack.ToString());
        }
    }
}