using System;
using System.Collections.Generic;
using AudioAdventurer.Library.Data.Objects;
using NUnit.Framework;

namespace AudioAdventurer.Library.Data.Tests.Objects
{
    [TestFixture]
    public class BehaviorDataTests
    {
        [Test]
        public void TestProperties()
        {
            var behaviorType = "TestBehavior";
            var id = Guid.NewGuid();
            var parentId = Guid.NewGuid();
            
            
            var properties = new Dictionary<string, string>();
            var key = "a";
            var value = "{ b: \"stuff\"}";
            properties.Add(key, value);


            var bd = new BehaviorData
            {
                BehaviorType = behaviorType,
                Id = id,
                ParentId = parentId,
                Properties = properties
            };

            Assert.AreEqual(behaviorType, bd.BehaviorType);
            Assert.AreEqual(id, bd.Id);
            Assert.AreEqual(parentId, bd.ParentId);
            Assert.AreEqual(properties, bd.Properties);
        }
    }
}
