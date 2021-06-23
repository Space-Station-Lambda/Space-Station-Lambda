using SSL_Core.gauges;
using SSL_Core.item.items;
using SSL_Core.player;
using Xunit;

namespace SSL_Core_Tests.item.items
{
    public class ItemFoodTests
    {
        [Fact]
        private void Should_Add_Feeding_To_Player()
        {
            Player player = new();
            Gauge gauge = new(new GaugeData("feeding"));
            ItemFood itemFood = new("test_food", "Food", 10);
            player.GaugeHandler.AddGauge(gauge);
            Assert.Equal(0, player.GaugeHandler.GetGaugeValue("feeding"));
            player.Use(itemFood);
            Assert.Equal(10, player.GaugeHandler.GetGaugeValue("feeding"));
        }
    }
}