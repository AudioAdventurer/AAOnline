using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioAdventurer.Library.Data.Objects;
using AudioAdventurer.Library.Testing.Helpers;
using NUnit.Framework;

namespace AudioAdventurer.Library.Data.Tests.Objects
{
    [TestFixture]
    public class ThingDataTests
    {
        [Test]
        public void TestProperties()
        {
            Guid id = Guid.NewGuid();
            string name = RandomHelper.GetRandomString(10);
            string fullName = RandomHelper.GetRandomString(10);
            string description = RandomHelper.GetRandomString(30);
            string title = RandomHelper.GetRandomString(20);
            string singularPrefix = RandomHelper.GetRandomString(5);
            string pluralPrefix = RandomHelper.GetRandomString(5);
            int maxChildren = RandomHelper.GetRandomInt(0, 100);
            int maxParents  = RandomHelper.GetRandomInt(0, 100);

            var td = new ThingData
            {
                Id = id,
                Name = name,
                FullName = fullName,
                Description = description,
                Title = title,
                SingularPrefix = singularPrefix,
                PluralSuffix = pluralPrefix,
                MaxParents = maxParents,
                MaxChildren = maxChildren
            };

            Assert.AreEqual(id, td.Id);
            Assert.AreEqual(name, td.Name);
            Assert.AreEqual(fullName, td.FullName);
            Assert.AreEqual(description, td.Description);
            Assert.AreEqual(title, td.Title);
            Assert.AreEqual(singularPrefix, td.SingularPrefix);
            Assert.AreEqual(pluralPrefix, td.PluralSuffix);
            Assert.AreEqual(maxChildren, td.MaxChildren);
            Assert.AreEqual(maxParents, td.MaxParents);
        }
    }
}
