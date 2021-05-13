using SSL_Core.item;
using SSL_Core.item.items;
using Xunit;

namespace SSL_Core_Tests.item
{
    public class ItemAuthorizerTests
    {
        private ItemAuthorizer itemAuthorizer;
        private Items items;
        public ItemAuthorizerTests()
        {
            items = new Items();
            items.Add(new ItemBag("test_bag", "Test de sac"));
            items.Add(new ItemBase("test_base", "Test de base"));
            items.Add(new ItemBase("test_base2", "Test de base2"));
            itemAuthorizer = new ItemAuthorizer(items);
        }

        [Fact]
        public void Should_All_Authorised_By_Default()
        {
            Assert.True(itemAuthorizer.IsAuthorized("test_bag"));
            Assert.True(itemAuthorizer.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_All_Unauthorised_By_Default_When_Selected()
        {
            itemAuthorizer = new ItemAuthorizer(items, false);
            Assert.False(itemAuthorizer.IsAuthorized("test_bag"));
            Assert.False(itemAuthorizer.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_All_Unauthorized()
        {
            itemAuthorizer.UnauthorizeAll();
            Assert.False(itemAuthorizer.IsAuthorized("test_bag"));
            Assert.False(itemAuthorizer.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_Not_Authorize()
        {
            itemAuthorizer.SetAuthorization("test_bag", false);
            Assert.False(itemAuthorizer.IsAuthorized("test_bag"));
            Assert.True(itemAuthorizer.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_All_Authorized()
        {
            itemAuthorizer.UnauthorizeAll();
            itemAuthorizer.AuthorizeAll();
            Assert.True(itemAuthorizer.IsAuthorized("test_bag"));
            Assert.True(itemAuthorizer.IsAuthorized("test_base"));
        }
        
        [Fact]
        public void Should_Unauthorize_By_Type()
        {
            itemAuthorizer.SetAuthorizationByType("base", false);
            Assert.True(itemAuthorizer.IsAuthorized("test_bag"));
            Assert.False(itemAuthorizer.IsAuthorized("test_base"));
            Assert.False(itemAuthorizer.IsAuthorized("test_base2"));
        }
    }
}