using System;
using AudioAdventurer.Library.Data.Objects;
using NUnit.Framework;

namespace AudioAdventurer.Library.Data.Tests.Objects
{
    [TestFixture]
    public class RelationshipDataTests
    {
        [Test]
        public void TestProperties()
        {
            var id = Guid.NewGuid();
            var childId = Guid.NewGuid();
            var parentId = Guid.NewGuid();

            RelationshipData rd = new RelationshipData
            {
                Id = id,
                ChildId = childId,
                ParentId = parentId
            };

            Assert.AreEqual(id, rd.Id);
            Assert.AreEqual(childId, rd.ChildId);
            Assert.AreEqual(parentId, rd.ParentId);
        }

    }
}
