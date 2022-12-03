using System.Linq;
using AudioAdventurer.Library.Adventure.Builders;
using AudioAdventurer.Library.Testing.Services;
using NUnit.Framework;

namespace AudioAdventurer.Library.Common.Tests.Models
{
    [TestFixture]
    public class ThingTests
    {
        [Test]
        public void TestAddChild()
        {
            InMemoryThingService thingService = new InMemoryThingService();
            
            var roomA = thingService.BuildRoom("Room A");
            var candle = thingService.BuildMoveableItem("A candle");

            roomA.AddChild(candle);

            Assert.IsTrue(roomA.Children.Contains(candle.Id));
        }

        [Test]
        public void TestRemoveChild()
        {
            InMemoryThingService thingService = new InMemoryThingService();

            var roomA = thingService.BuildRoom("Room A");
            var candle = thingService.BuildMoveableItem("A candle");

            roomA.AddChild(candle);
            Assert.IsTrue(roomA.Children.Contains(candle.Id));

            roomA.RemoveChild(candle);
            Assert.IsFalse(roomA.Children.Contains(candle.Id));
        }
    }
}
