using SSL.gauges;
using SSL.item.items;
using SSL.player;
using Xunit;

namespace SSL_Tests.item.items
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
            itemFood.Use(player);
            Assert.Equal(10, player.GaugeHandler.GetGaugeValue("feeding"));
        }
    }
}
