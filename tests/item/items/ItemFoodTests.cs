using SSL.Gauge;
using SSL.Item.items;
using SSL.Player;
using Xunit;

namespace SSL.Tests.Item.items
{
    public class ItemFoodTests
    {
        [Fact]
        private void Should_Add_Feeding_To_Player()
        {
            MainPlayer player = new();
            Gauge.Gauge gauge = new(new GaugeData("feeding"));
            ItemFood itemFood = new("test_food", "Food", 10);
            player.GaugeHandler.AddGauge(gauge);
            Assert.Equal(0, player.GaugeHandler.GetGaugeValue("feeding"));
            itemFood.Use(player);
            Assert.Equal(10, player.GaugeHandler.GetGaugeValue("feeding"));
        }
    }
}
