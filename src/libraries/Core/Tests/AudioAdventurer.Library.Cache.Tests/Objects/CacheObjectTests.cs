using System;
using AudioAdventurer.Library.Cache.Objects;
using AudioAdventurer.Library.Common.Models;
using AudioAdventurer.Library.Testing.Helpers;
using NUnit.Framework;

namespace AudioAdventurer.Library.Cache.Tests.Objects
{
    [TestFixture]
    public class CacheObjectTests
    {
        [Test]
        public void TestProperties()
        {
            var id = Guid.NewGuid();
            var value = new ExitDestination()
            {
                TargetId = Guid.NewGuid(),
                ExitCommand = RandomHelper.GetRandomString(1)
            };


            var cacheObject = new CacheObject<ExitDestination>(
                id,
                value);

            Assert.AreEqual(id, cacheObject.Id);
            Assert.NotNull(cacheObject.Value);
            Assert.AreEqual(value.TargetId, cacheObject.Value.TargetId);
            Assert.AreEqual(value.ExitCommand, cacheObject.Value.ExitCommand);
        }
    }
}
