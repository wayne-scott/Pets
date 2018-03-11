using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pets.WebSite.Models;

namespace Pets.WebSite.Tests.Models
{
    [TestClass]
    public class PetGroupsTests
    {
        [TestMethod]
        public void CreatePetGroupsObject()
        {
            var petGroups = new PetGroups();
            Assert.IsNotNull(petGroups);
        }

        [TestMethod]
        public void PetGroupsWithNoPets()
        {
            var petGroups = new PetGroups();
            Assert.IsFalse(petGroups.HasGroups);
            Assert.AreEqual(0, petGroups.Collection.Count);
        }

        [TestMethod]
        public void PetGroupsWithPets()
        {
            var petGroups = new PetGroups();
            petGroups.Collection.Add(new WebSite.Pages.Shared.Pets());
            Assert.IsTrue(petGroups.HasGroups);
            Assert.AreEqual(1, petGroups.Collection.Count);
        }

        [TestMethod]
        public void CheckGroupByProperty()
        {
            var petGroups = new PetGroups();
            Assert.IsNotNull(petGroups);
            petGroups.GroupedBy = "Age";
            Assert.AreEqual("Age", petGroups.GroupedBy);
        }
    }
}
