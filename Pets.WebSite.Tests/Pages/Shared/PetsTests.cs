using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pets.WebSite.Tests.Pages.Shared
{
    [TestClass]
    public class PetsTests
    {
        [TestMethod]
        public void CreateObject()
        {
            var pets = new WebSite.Pages.Shared.Pets();
            Assert.IsNotNull(pets);
            Assert.IsTrue(string.IsNullOrEmpty(pets.Title));
            Assert.IsNotNull(pets.Collection);
            Assert.AreEqual(0, pets.Collection.Count);
        }

        [TestMethod]
        public void PetsWithTitle()
        {
            var pets = new WebSite.Pages.Shared.Pets { Title = "Cat1" };
            Assert.IsNotNull(pets);
            Assert.AreEqual("Cat1", pets.Title);
        }
    }
}
