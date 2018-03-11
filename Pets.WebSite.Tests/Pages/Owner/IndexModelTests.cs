using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pets.DAL;
using Pets.DomainModel;
using Pets.WebSite.Pages.Owner;

namespace Pets.WebSite.Tests.Pages.Owner
{
    [TestClass]
    public class IndexModelTests
    {
        [TestMethod]
        public void OnGet()
        {
            var repository = Substitute.For<IOwnerRepository>();
            var owners = new Owners();
            owners.Initialise(new List<DomainModel.Owner> { new DomainModel.Owner { Name = "John" }});
            repository.GetAllOwners().Returns(owners);

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet(string.Empty);

            Assert.IsNull(indexModel.Owner);
            Assert.IsNotNull(indexModel.Owners);
            Assert.AreEqual(1, indexModel.Owners.Count);
            Assert.AreEqual(string.Empty, indexModel.Message);
            repository.Received(1).GetAllOwners();
        }

        [TestMethod]
        public void OnGetNoData()
        {
            var repository = Substitute.For<IOwnerRepository>();
            repository.GetAllOwners().Returns(new Owners());

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet(string.Empty);

            Assert.IsNull(indexModel.Owner);
            Assert.IsNull(indexModel.Owners);
            Assert.AreEqual("No owners found?", indexModel.Message);
            repository.Received(1).GetAllOwners();
        }

        [TestMethod]
        public void OnGetWithOwnerName()
        {
            var repository = Substitute.For<IOwnerRepository>();
            var owners = new Owners();
            owners.Initialise(new List<DomainModel.Owner> { new DomainModel.Owner { Name = "John" } });
            repository.GetAllOwners().Returns(owners);

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet("John");

            Assert.IsNotNull(indexModel.Owner);
            Assert.IsNull(indexModel.Owners);
            Assert.AreEqual("John", indexModel.Owner.Name);
            Assert.AreEqual(string.Empty, indexModel.Message);
            repository.Received(1).GetAllOwners();
        }

        [TestMethod]
        public void OnGetWithOwnerNameNoMatch()
        {
            var repository = Substitute.For<IOwnerRepository>();
            var owners = new Owners();
            owners.Initialise(new List<DomainModel.Owner> { new DomainModel.Owner { Name = "John" } });
            repository.GetAllOwners().Returns(owners);

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet("Fred");

            Assert.IsNull(indexModel.Owner);
            Assert.IsNull(indexModel.Owners);
            Assert.AreEqual("Fred not found?", indexModel.Message);
            repository.Received(1).GetAllOwners();
        }
    }
}
