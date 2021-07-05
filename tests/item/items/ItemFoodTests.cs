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
            MainPlayer player = new();
            Gauge.Gauge gauge = new(new GaugeData("feeding"));
            ItemFood itemFood = new("test_food", "Food", 10);
            player.GaugeHandler.AddGauge(gauge);
            Assert.Equal(0, player.GaugeHandler.GetGaugeValue("feeding"));
            //player.Use(itemFood); FIXME
            Assert.Equal(10, player.GaugeHandler.GetGaugeValue("feeding"));
        }
    }
}
