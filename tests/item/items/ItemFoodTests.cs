using Moq;
using Moq;
using ssl.Gauge;
using ssl.Item.ItemTypes;
using ssl.Player;
using Xunit;

namespace ssl.Tests.Item.items
{
    public class ItemFoodTests
    {
        [Fact]
        private void Should_Add_Feeding_To_Player()
        {
            Mock<MainPlayer> mockPlayer = new();
            Gauge.Gauge gauge = new(new GaugeData("feeding"));
            ItemFood itemFood = new("test_food", "Food", "", 10);
            mockPlayer.Object.GaugeHandler.AddGauge(gauge);

            Assert.Equal(0, mockPlayer.Object.GaugeHandler.GetGaugeValue("feeding"));

            mockPlayer.Object.Use(itemFood);

            Assert.Equal(10, mockPlayer.Object.GaugeHandler.GetGaugeValue("feeding"));
        }
    }
}