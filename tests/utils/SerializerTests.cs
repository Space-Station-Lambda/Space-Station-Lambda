using SSL.item.items;
using SSL.utils;
using Xunit;
using Xunit.Abstractions;

namespace SSL_Tests.utils
{
    public class SerializerTests
    {

        [Fact]
        public void Should_Deserialize()
        {
            string serializedString =
                "<Item xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" i:type=\"ItemFood\" xmlns=\"items\">"
                + "<DestroyOnUse>false</DestroyOnUse>"
                + "<Id>test_base</Id>"
                + "<MaxStack>100</MaxStack>"
                + "<Name>Test</Name>"
                + "<FeedingValue>4</FeedingValue>"
                + "</Item>";
            Serializer serializer = new();
            ItemFood item = serializer.Deserialize<ItemFood>(serializedString);
            Assert.False(item.DestroyOnUse);
            Assert.Equal("test_base", item.Id);
            Assert.Equal(100, item.MaxStack);
            Assert.Equal("Test", item.Name);
            Assert.Equal(4, item.FeedingValue);
        }
        
        [Fact]
        public void Should_Serialize()
        {
            Item item = new ItemFood("test_base", "Test", 4);
            Serializer serializer = new();
            string serializedString = serializer.Serialize(item);
            Assert.Contains("<DestroyOnUse>false</DestroyOnUse>", serializedString);
            Assert.Contains("<Id>test_base</Id>", serializedString);
            Assert.Contains("<MaxStack>100</MaxStack>", serializedString);
            Assert.Contains("<Name>Test</Name>", serializedString);
            Assert.Contains("<FeedingValue>4</FeedingValue>", serializedString);
            Assert.Contains("<Item xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" i:type=\"ItemFood\" xmlns=\"items\">", serializedString);
        }
    }
}
