using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Pets.DomainModel.Tests
{
    [TestClass]
    public class PetTests
    {
        private const string TEST_PET_JSON = "{'name':'Tigger','type':'Cat'}";

        [TestMethod]
        public void CreatePetObject()
        {
            var pet = new Pet();
            Assert.IsNotNull(pet);
        }

        [TestMethod]
        public void PetIsACat()
        {
            var pet = new Pet
            {
                Name = "Tigger",
                Type = "Cat"
            };
            Assert.AreEqual("Tigger", pet.Name);
            Assert.AreEqual("Cat", pet.Type);
            Assert.IsTrue(pet.IsCat);
        }

        [TestMethod]
        public void PetIsNotACat()
        {
            var pet = new Pet
            {
                Name = "Arnold",
                Type = "Aardvark"
            };
            Assert.AreEqual("Arnold", pet.Name);
            Assert.AreEqual("Aardvark", pet.Type);
            Assert.IsFalse(pet.IsCat);
        }

        [TestMethod]
        public void LoadPetFromJson()
        {
            var pet = JsonConvert.DeserializeObject<Pet>(TEST_PET_JSON);

            Assert.AreEqual("Tigger", pet.Name);
            Assert.AreEqual("Cat", pet.Type);
        }
    }
}
