using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Pets.DomainModel.Tests
{
    [TestClass]
    public class OwnersTests
    {
        [TestMethod]
        public void CreateOwnersObject()
        {
            var owners = new Owners();
            Assert.IsNotNull(owners);
        }

        [TestMethod]
        public void CheckThatAnUnitialisedObjectWontError()
        {
            var owners = new Owners();
            Assert.AreEqual(0, owners.PetOwners.Count);
            Assert.AreEqual(0, owners.CatOwners.Count());
        }

        [TestMethod]
        public void CheckThatTheCatListOnlyContiansCats()
        {
            var ownerWithCat = new Owner();
            ownerWithCat.Pets.Add(new Pet { Name = "Tigger", Type = "Cat" });
            var ownerWithOutCat = new Owner();
            ownerWithOutCat.Pets.Add(new Pet { Name = "Arnold", Type = "Aardvark" });

            var listOfOwners = new List<Owner>
            {
                ownerWithCat,
                ownerWithOutCat
            };

            var owners = new Owners();
            owners.Initialise(listOfOwners);

            Assert.AreEqual(2, owners.PetOwners.Count);
            Assert.AreEqual(1, owners.CatOwners.Count());
        }
    }
}
