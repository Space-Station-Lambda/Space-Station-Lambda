using System;
using SSL_Core.item;
using SSL_Core.item.items;
using Xunit;

namespace SSL_Core_Tests.item
{
    public class InventoryTests
    {
        private Inventory inventory;
        
        private IItems authorizedItems;
        
        private Item item;
        
        public InventoryTests()
        {
            item = new ItemBase("test", "Test Item", 16);
            authorizedItems = new Items();
            authorizedItems.Add(item);
            inventory = new Inventory(authorizedItems, 4);
        }
        
        [Fact]
        private void Should_AddItem_When_EmptyInventory()
        {
            var itemStack = new ItemStack(item, 1);
            
            inventory.AddItem(itemStack);
            
            Assert.Equal(1, inventory.SlotsFull);
            Assert.Equal(3, inventory.SlotsLeft);
            Assert.Equal(itemStack, inventory.Items[0]);
        }

        [Fact]
        private void Should_AddItemOnAnotherSlot_When_Not_EmptyInventory()
        {
            var stack1 = new ItemStack(item, 1);
            var stack2 = new ItemStack(item, 1);
            
            inventory.AddItem(stack1);
            inventory.AddItem(stack2, 1);
            
            Assert.Equal(2, inventory.SlotsFull);
            Assert.Equal(2, inventory.SlotsLeft);
            Assert.Equal(stack1, inventory.Items[0]);
            Assert.Equal(stack2, inventory.Items[1]);
        }
        
        [Fact]
        private void Should_AddItemOnSameSlot_When_ItemAlreadyPresent()
        {
            var stack1 = new ItemStack(item, 1);
            var stack2 = new ItemStack(item, 1);
            
            inventory.AddItem(stack1);
            inventory.AddItem(stack2);
            
            Assert.Equal(1, inventory.SlotsFull);
            Assert.Equal(3, inventory.SlotsLeft);
            Assert.Equal(stack1, inventory.Items[0]);
            Assert.Equal(2, inventory.Items[0].Amount);
        }
        
        [Fact]
        private void Should_AddItemOnFirstAvailableSlot_When_ItemAlreadyPresent()
        {
            var item2 = new ItemBase("better-test", "Better Test Item");
            authorizedItems.Add(item2);
            inventory = new Inventory(authorizedItems, 4);
            var stack1 = new ItemStack(item, 1);
            var stack2 = new ItemStack(item2, 1);
            
            inventory.AddItem(stack1);
            inventory.AddItem(stack2);
            
            Assert.Equal(2, inventory.SlotsFull);
            Assert.Equal(2, inventory.SlotsLeft);
            Assert.Equal(stack1, inventory.Items[0]);
            Assert.Equal(stack2, inventory.Items[1]);
        }
        
        [Fact]
        private void Should_ThrowFullException_When_FullInventoryDifferentItem()
        {
            var item2 = new ItemBase("better-test", "Better Test Item");
            authorizedItems.Add(item2);
            inventory = new Inventory(authorizedItems, 2);
            var stack1 = new ItemStack(item, 1);
            var stack2 = new ItemStack(item, 1);
            var stack3 = new ItemStack(item2, 1);
            
            inventory.AddItem(stack1);
            inventory.AddItem(stack2, 1);
            
            Assert.Throws<FullInventoryException>(() => inventory.AddItem(stack3));
        }
        
        [Fact]
        private void Should_ThrowFullException_When_FullInventorySameItem()
        {
            var stack1 = new ItemStack(item, 16);
            var stack2 = new ItemStack(item, 16);            
            var stack3 = new ItemStack(item, 16);
            var stack4 = new ItemStack(item, 16);
            var stack5 = new ItemStack(item, 1);
            
            inventory.AddItem(stack1);
            inventory.AddItem(stack2, 1);
            inventory.AddItem(stack3, 2);
            inventory.AddItem(stack4, 3);
            
            Assert.Throws<FullInventoryException>(() => inventory.AddItem(stack5));
        }
        
        [Fact]
        private void AddItem_Should_ThrowException_When_OutOfBounds()
        {
            var stack = new ItemStack(item);
            
            Assert.Throws<IndexOutOfRangeException>(() => inventory.AddItem(stack, 80));
            Assert.Throws<IndexOutOfRangeException>(() => inventory.AddItem(stack, -1));
        }

        [Fact]
        private void Should_RemoveItem_When_Not_EmptyInventory()
        {
            var stack1 = new ItemStack(item, 2);
            inventory.AddItem(stack1);

            var removedStack = inventory.RemoveItem(0);
            
            Assert.Equal(0, inventory.SlotsFull);
            Assert.Equal(4, inventory.SlotsLeft);
            Assert.Equal(stack1, removedStack);
            Assert.Null(inventory.Items[0]);
        }

        [Fact]
        private void Should_ReturnNull_When_EmptySlot()
        {
            var stack1 = new ItemStack(item, 2);
            inventory.AddItem(stack1);

            var removedStack = inventory.RemoveItem(1);
            
            Assert.Equal(1, inventory.SlotsFull);
            Assert.Equal(3, inventory.SlotsLeft);
            Assert.Null(removedStack);
            Assert.Null(inventory.Items[1]);
        }
        
        [Fact]
        private void RemoveItem_Should_ThrowException_When_OutOfBounds()
        {
            Assert.Throws<IndexOutOfRangeException>(() => inventory.RemoveItem(80));
            Assert.Throws<IndexOutOfRangeException>(() => inventory.RemoveItem(-1));
        }

        [Fact]
        private void IsPresent_Should_ReturnFalse()
        {
            var stack1 = new ItemStack(item);
            inventory.AddItem(stack1);
            var stack2 = new ItemStack(item);
            
            Assert.False(inventory.IsPresent(stack2));
        }
        
        [Fact]
        private void IsPresent_Should_ReturnTrue()
        {
            var stack1 = new ItemStack(item);
            inventory.AddItem(stack1);
            
            Assert.True(inventory.IsPresent(stack1));
        }

        [Fact]
        private void IsSlotEmpty_Should_ReturnTrue()
        {
            Assert.True(inventory.IsSlotEmpty(0));
        }

        [Fact]
        private void IsSlotEmpty_Should_ReturnFalse()
        {
            var stack1 = new ItemStack(item);
            inventory.AddItem(stack1);

            Assert.False(inventory.IsSlotEmpty(0));
        }

        [Fact]
        private void IsSlotEmpty_Should_ThrowException_When_OutOfBounds()
        {
            Assert.Throws<IndexOutOfRangeException>(() => inventory.IsSlotEmpty(-1));
            Assert.Throws<IndexOutOfRangeException>(() => inventory.IsSlotEmpty(80));
        }

        // TODO: Unit tests when item unauthorized
    }
}