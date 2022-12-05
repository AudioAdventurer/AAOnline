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
            Assert.IsTrue(candle.Parents.Contains(roomA.Id));

            roomA.RemoveChild(candle);
            Assert.IsFalse(roomA.Children.Contains(candle.Id));
            Assert.IsFalse(candle.Parents.Contains(roomA.Id));
        }

        [Test]
        public void TestRemoveChildRepeat()
        {
            InMemoryThingService thingService = new InMemoryThingService();

            var roomA = thingService.BuildRoom("Room A");
            var candle = thingService.BuildMoveableItem("A candle");

            roomA.AddChild(candle);
            Assert.IsTrue(roomA.Children.Contains(candle.Id));
            Assert.IsTrue(candle.Parents.Contains(roomA.Id));

            roomA.RemoveChild(candle);
            Assert.IsFalse(roomA.Children.Contains(candle.Id));
            Assert.IsFalse(candle.Parents.Contains(roomA.Id));

            roomA.AddChild(candle);
            Assert.IsTrue(roomA.Children.Contains(candle.Id));
            Assert.IsTrue(candle.Parents.Contains(roomA.Id));

            roomA.RemoveChild(candle);
            Assert.IsFalse(roomA.Children.Contains(candle.Id));
            Assert.IsFalse(candle.Parents.Contains(roomA.Id));
        }

        [Test]
        public void TestRemoveParent()
        {
            InMemoryThingService thingService = new InMemoryThingService();

            var roomA = thingService.BuildRoom("Room A");
            var candle = thingService.BuildMoveableItem("A candle");

            roomA.AddChild(candle);
            Assert.IsTrue(roomA.Children.Contains(candle.Id));
            Assert.IsTrue(candle.Parents.Contains(roomA.Id));

            candle.RemoveParent(roomA);
            Assert.IsFalse(roomA.Children.Contains(candle.Id));
            Assert.IsFalse(candle.Parents.Contains(roomA.Id));
        }

        [Test]
        public void TestSwitchParent()
        {
            InMemoryThingService thingService = new InMemoryThingService();

            var roomA = thingService.BuildRoom("Room A");
            var roomB = thingService.BuildRoom("Room B");
            var candle = thingService.BuildMoveableItem("A candle");

            roomA.AddChild(candle);
            Assert.IsTrue(roomA.Children.Contains(candle.Id));
            Assert.IsTrue(candle.Parents.Contains(roomA.Id));

            roomB.AddChild(candle);
            Assert.IsFalse(roomA.Children.Contains(candle.Id));
            Assert.IsFalse(candle.Parents.Contains(roomA.Id));
            Assert.IsTrue(roomB.Children.Contains(candle.Id));
            Assert.IsTrue(candle.Parents.Contains(roomB.Id));
        }

        [Test]
        public void TestMultipleChildren()
        {
            InMemoryThingService thingService = new InMemoryThingService();

            var roomA = thingService.BuildRoom("Room A");
            var candle = thingService.BuildMoveableItem("A candle");
            var stick = thingService.BuildMoveableItem("A stick");

            roomA.AddChild(candle);
            roomA.AddChild(stick);
            Assert.IsTrue(roomA.Children.Contains(candle.Id));
            Assert.IsTrue(candle.Parents.Contains(roomA.Id));
            Assert.IsTrue(roomA.Children.Contains(stick.Id));
            Assert.IsTrue(stick.Parents.Contains(roomA.Id));
        }

        [Test]
        public void TestRemoveFirstChild()
        {
            InMemoryThingService thingService = new InMemoryThingService();

            var roomA = thingService.BuildRoom("Room A");
            var candle = thingService.BuildMoveableItem("A candle");
            var stick = thingService.BuildMoveableItem("A stick");

            roomA.AddChild(candle);
            roomA.AddChild(stick);
            Assert.IsTrue(roomA.Children.Contains(candle.Id));
            Assert.IsTrue(candle.Parents.Contains(roomA.Id));
            Assert.IsTrue(roomA.Children.Contains(stick.Id));
            Assert.IsTrue(stick.Parents.Contains(roomA.Id));

            roomA.RemoveChild(candle);
            Assert.IsFalse(roomA.Children.Contains(candle.Id));
            Assert.IsFalse(candle.Parents.Contains(roomA.Id));
            Assert.IsTrue(roomA.Children.Contains(stick.Id));
            Assert.IsTrue(stick.Parents.Contains(roomA.Id));
        }

        [Test]
        public void TestRemoveSecondChild()
        {
            InMemoryThingService thingService = new InMemoryThingService();

            var roomA = thingService.BuildRoom("Room A");
            var candle = thingService.BuildMoveableItem("A candle");
            var stick = thingService.BuildMoveableItem("A stick");

            roomA.AddChild(candle);
            roomA.AddChild(stick);
            Assert.IsTrue(roomA.Children.Contains(candle.Id));
            Assert.IsTrue(candle.Parents.Contains(roomA.Id));
            Assert.IsTrue(roomA.Children.Contains(stick.Id));
            Assert.IsTrue(stick.Parents.Contains(roomA.Id));

            roomA.RemoveChild(stick);
            Assert.IsTrue(roomA.Children.Contains(candle.Id));
            Assert.IsTrue(candle.Parents.Contains(roomA.Id));
            Assert.IsFalse(roomA.Children.Contains(stick.Id));
            Assert.IsFalse(stick.Parents.Contains(roomA.Id));
        }
    }
}
