using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Models;
using NUnit.Framework;
using RandomHelper = AudioAdventurer.Library.Testing.Helpers.RandomHelper;

namespace AudioAdventurer.Library.Common.Tests.Helpers
{
    [TestFixture]
    public class PropertyHelperTests
    {
        [Test]
        public void TestString()
        {
            string value = RandomHelper.GetRandomString(10);
            
            Dictionary<string, string> properties = new Dictionary<string, string>();

            properties.SetValue("key", value);

            var testValue = properties.GetStringValue("key");
            Assert.AreEqual(value, testValue);

            var testNull = properties.GetStringValue("missingKey");
            Assert.IsNull(testNull);

            string defaultValue = RandomHelper.GetRandomString(10);
            var testDefault = properties.GetStringValue("missingKey", defaultValue);

            Assert.AreEqual(defaultValue, testDefault);
        }

        [Test]
        public void TestDouble()
        {
            var value = RandomHelper.GetRandomDouble(0, 100);

            Dictionary<string, string> properties = new Dictionary<string, string>();

            properties.SetValue("key", value);

            var testValue = properties.GetDoubleValue("key");
            Assert.AreEqual(value, testValue);

            var testEmpty = properties.GetDoubleValue("missingKey");
            Assert.AreEqual(0.0, testEmpty);

            var defaultValue = RandomHelper.GetRandomDouble(150, 300);
            var testDefault = properties.GetDoubleValue("missingKey", defaultValue);

            Assert.AreEqual(defaultValue, testDefault);
        }

        [Test]
        public void TestInt()
        {
            var value = RandomHelper.GetRandomInt(0, 100);

            Dictionary<string, string> properties = new Dictionary<string, string>();

            properties.SetValue("key", value);

            var testValue = properties.GetIntValue("key");
            Assert.AreEqual(value, testValue);

            var testEmpty = properties.GetIntValue("missingKey");
            Assert.AreEqual(0.0, testEmpty);

            var defaultValue = RandomHelper.GetRandomInt(150, 300);
            var testDefault = properties.GetIntValue("missingKey", defaultValue);

            Assert.AreEqual(defaultValue, testDefault);
        }

        [Test]
        public void TestObject()
        {
            var value = new ExitDestination()
            {
                TargetId = Guid.NewGuid(),
                ExitCommand = RandomHelper.GetRandomString(1)
            };

            Dictionary<string, string> properties = new Dictionary<string, string>();

            properties.SetSerializedJsonObject("key", value);

            var testValue = properties.GetSerializedJsonObject<ExitDestination>("key");
            Assert.NotNull(testValue);
            Assert.AreEqual(value.TargetId, testValue.TargetId);
            Assert.AreEqual(value.ExitCommand, testValue.ExitCommand);

            var testEmpty = properties.GetSerializedJsonObject<ExitDestination>("missingKey");
            Assert.IsNull(testEmpty);
        }

        [Test]
        public void TestObjects()
        {
            var value1 = new ExitDestination()
            {
                TargetId = Guid.NewGuid(),
                ExitCommand = RandomHelper.GetRandomString(1)
            };

            var value2 = new ExitDestination()
            {
                TargetId = Guid.NewGuid(),
                ExitCommand = RandomHelper.GetRandomString(1)
            };

            List<ExitDestination> destinations = new List<ExitDestination>
            {
                value1,
                value2
            };

            Dictionary<string, string> properties = new Dictionary<string, string>();

            properties.SetSerializedJsonObjects("destinations", destinations);

            var values = properties.GetSerializedJsonObjects<ExitDestination>("destinations");

            var testValue1 = values[0];
            Assert.NotNull(testValue1);
            Assert.AreEqual(value1.TargetId, testValue1.TargetId);
            Assert.AreEqual(value1.ExitCommand, testValue1.ExitCommand);

            var testValue2 = values[1];
            Assert.NotNull(testValue2);
            Assert.AreEqual(value2.TargetId, testValue2.TargetId);
            Assert.AreEqual(value2.ExitCommand, testValue2.ExitCommand);

            var testEmpty = properties.GetSerializedJsonObjects<ExitDestination>("missingKey");
            Assert.IsNotNull(testEmpty);
            Assert.IsEmpty(testEmpty);
        }
    }
}
