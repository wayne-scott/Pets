using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;

namespace Pets.DomainModel.Tests
{
    [TestClass]
    public class OwnerTests
    {
        private const string TEST_OWNER_JSON = "{'name':'Steve','gender':'Male','age':45}";
        private const string TEST_PET_OWNER_JSON = "{'name':'Steve','gender':'Male','age':45,'pets':[{'name':'Tigger','type':'Cat'}]}";

        [TestMethod]
        public void CreateOwnerObject()
        {
            var owner = new Owner();
            Assert.IsNotNull(owner);
        }

        [TestMethod]
        public void ReadAndWriteProperties()
        {
            var owner = new Owner
            {
                Name = "Fred",
                Age = 21,
                Gender = "Male"
            };

            Assert.AreEqual("Fred", owner.Name);
            Assert.AreEqual(21, owner.Age);
            Assert.AreEqual("Male", owner.Gender);
        }

        [TestMethod]
        public void OwnerHasNoPets()
        {
            var owner = new Owner();
            Assert.IsNotNull(owner.Pets);
            Assert.AreEqual(0, owner.Pets.Count);
            Assert.IsFalse(owner.HasCats);
            Assert.IsNotNull(owner.Cats);
            Assert.AreEqual(0, owner.Cats.Count());
        }

        [TestMethod]
        public void OwnerHasACat()
        {
            var owner = new Owner();
            owner.Pets.Add(new Pet { Name = "Tigger", Type = "Cat" });
            Assert.IsNotNull(owner.Pets);
            Assert.AreEqual(1, owner.Pets.Count);
            Assert.IsTrue(owner.HasCats);
            Assert.IsNotNull(owner.Cats);
            Assert.AreEqual(1, owner.Cats.Count());
        }

        [TestMethod]
        public void OwnerDoesNotHaveACat()
        {
            var owner = new Owner();
            owner.Pets.Add(new Pet { Name = "Arnold", Type = "Aardvark" });
            Assert.IsNotNull(owner.Pets);
            Assert.AreEqual(1, owner.Pets.Count);
            Assert.IsFalse(owner.HasCats);
            Assert.IsNotNull(owner.Cats);
            Assert.AreEqual(0, owner.Cats.Count());
        }

        [TestMethod]
        public void LoadOwnerFromJson()
        {
            var owner = JsonConvert.DeserializeObject<Owner>(TEST_OWNER_JSON);

            Assert.AreEqual("Steve", owner.Name);
            Assert.AreEqual(45, owner.Age);
            Assert.AreEqual("Male", owner.Gender);
        }

        [TestMethod]
        public void LoadOwnerWithPetsFromJson()
        {
            var owner = JsonConvert.DeserializeObject<Owner>(TEST_PET_OWNER_JSON);

            Assert.AreEqual("Steve", owner.Name);
            Assert.AreEqual(45, owner.Age);
            Assert.AreEqual("Male", owner.Gender);
            Assert.IsNotNull(owner.Pets);
            Assert.AreEqual(1, owner.Pets.Count);
        }
    }
}
