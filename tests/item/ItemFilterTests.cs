using ssl.Item;
using ssl.Item.ItemTypes;
using Xunit;

namespace ssl.Tests.Item
{
    public class ItemFilterTests
    {
        private ItemFilter itemFilter;
        private Items items;
        public ItemFilterTests()
        {
            items = new Items();
            items.Add(new ItemBag("test_bag", "Test de sac", ""));
            items.Add(new ItemBase("test_base", "Test de base", ""));
            items.Add(new ItemBase("test_base2", "Test de base2", ""));
            itemFilter = new ItemFilter(items);
        }

        [Fact]
        public void Should_All_Authorised_By_Default()
        {
            Assert.True(itemFilter.IsAuthorized("test_bag"));
            Assert.True(itemFilter.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_All_Unauthorised_By_Default_When_Selected()
        {
            itemFilter = new ItemFilter(items, false);
            Assert.False(itemFilter.IsAuthorized("test_bag"));
            Assert.False(itemFilter.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_All_Unauthorized()
        {
            itemFilter.UnauthorizeAll();
            Assert.False(itemFilter.IsAuthorized("test_bag"));
            Assert.False(itemFilter.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_Not_Authorize()
        {
            itemFilter.SetAuthorization("test_bag", false);
            Assert.False(itemFilter.IsAuthorized("test_bag"));
            Assert.True(itemFilter.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_All_Authorized()
        {
            itemFilter.UnauthorizeAll();
            itemFilter.AuthorizeAll();
            Assert.True(itemFilter.IsAuthorized("test_bag"));
            Assert.True(itemFilter.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_Unauthorize_By_Type()
        {
            itemFilter.SetAuthorizationByType("base", false);
            Assert.True(itemFilter.IsAuthorized("test_bag"));
            Assert.False(itemFilter.IsAuthorized("test_base"));
            Assert.False(itemFilter.IsAuthorized("test_base2"));
        }
    }
}
