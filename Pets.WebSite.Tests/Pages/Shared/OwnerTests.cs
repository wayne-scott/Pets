using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pets.WebSite.Tests.Pages.Shared
{
    [TestClass]
    public class OwnerTests
    {
        [TestMethod]
        public void CreateOwnerObject()
        {
            var model = new WebSite.Pages.Shared.Owner();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void TestOwnerProperties()
        {
            var model = new WebSite.Pages.Shared.Owner { Name = "John", Age = 44, Gender = "Male" };
            Assert.AreEqual("John", model.Name);
            Assert.AreEqual(44, model.Age);
            Assert.AreEqual("Male", model.Gender);
        }
    }
}
