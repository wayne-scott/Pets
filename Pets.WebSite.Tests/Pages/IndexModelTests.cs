using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pets.DAL;
using Pets.DomainModel;
using Pets.WebSite.Pages;

namespace Pets.WebSite.Tests.Pages
{
    [TestClass]
    public class IndexModelTests
    {
        [TestMethod]
        public void OnGet()
        {
            var repository = Substitute.For<IOwnerRepository>();
            repository.GetAllOwners().Returns(new Owners());

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet();

            Assert.IsNotNull(indexModel.GroupOfPets);
            Assert.AreEqual(0, indexModel.GroupOfPets.Collection.Count);
            repository.Received(1).GetAllOwners();
        }

        [TestMethod]
        public void OnGetNoData()
        {
            var repository = Substitute.For<IOwnerRepository>();
            Owners owners = null;
            repository.GetAllOwners().Returns(owners);

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet();

            Assert.IsNotNull(indexModel.GroupOfPets);
            Assert.AreEqual(0, indexModel.GroupOfPets.Collection.Count);
            repository.Received(1).GetAllOwners();
        }
    }
}
