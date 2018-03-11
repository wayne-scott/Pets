using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pets.DomainModel;
using Pets.WebSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pets.WebSite.Tests.Models
{
    [TestClass]
    public class ModelFactoryTest
    {
        [TestMethod]
        public void CreateModelFactoryObject()
        {
            var factory = new ModelFactory();
            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void CreatePetGroupsModelWithNoData()
        {
            var factory = new ModelFactory();
            var model = factory.Create(new List<Owner>().GroupBy(owner => owner.Gender, owner => owner.Cats));
            Assert.IsNotNull(model);
            Assert.IsFalse(model.HasGroups);
            Assert.AreEqual(0, model.Collection.Count);
        }

        [TestMethod]
        public void CreatePetGroupsModelWithAnOwnerWithACat()
        {
            var factory = new ModelFactory();
            var testOwner = new Owner { Gender = "Male" };
            testOwner.Pets.Add(new Pet { Name = "Tigger", Type = "Cat" });
            var results = new List<Owner> { testOwner };
            var petGroups = factory.Create(results.GroupBy(owner => owner.Gender, owner => owner.Cats));

            Assert.IsNotNull(petGroups);
            Assert.IsTrue(petGroups.HasGroups);
            Assert.AreEqual(1, petGroups.Collection.Count);
            Assert.AreEqual("Male", petGroups.Collection[0].Title);
            Assert.AreEqual(1, petGroups.Collection[0].Collection.Count);
            Assert.AreEqual("Tigger", petGroups.Collection[0].Collection[0].Name);
        }

        [TestMethod]
        public void CreatePetGroupsModelWithAnOwnerWithTwoCats()
        {
            var factory = new ModelFactory();
            var testOwner = new Owner { Gender = "Male" };
            testOwner.Pets.Add(new Pet { Name = "Tigger", Type = "Cat" });
            testOwner.Pets.Add(new Pet { Name = "Jerry", Type = "Cat" });
            var results = new List<Owner> { testOwner };
            var model = factory.Create(results.GroupBy(owner => owner.Gender, owner => owner.Cats));

            Assert.IsNotNull(model);
            Assert.IsTrue(model.HasGroups);
            Assert.AreEqual(1, model.Collection.Count);
            Assert.AreEqual("Male", model.Collection[0].Title);
            Assert.AreEqual(2, model.Collection[0].Collection.Count);
            Assert.AreEqual("Jerry", model.Collection[0].Collection[0].Name);
            Assert.AreEqual("Tigger", model.Collection[0].Collection[1].Name);
        }

        [TestMethod]
        public void CreatePetGroupsWithTwoOwnersWithACat()
        {
            var factory = new ModelFactory();
            var testOwner = new Owner { Gender = "Male" };
            testOwner.Pets.Add(new Pet { Name = "Tigger", Type = "Cat" });
            var testOwner1 = new Owner { Gender = "Female" };
            testOwner1.Pets.Add(new Pet { Name = "Jerry", Type = "Cat" });
            var results = new List<Owner> { testOwner, testOwner1 };
            var model = factory.Create(results.GroupBy(owner => owner.Gender, owner => owner.Cats));

            Assert.IsNotNull(model);
            Assert.IsTrue(model.HasGroups);
            Assert.AreEqual(2, model.Collection.Count);
            Assert.AreEqual("Male", model.Collection[0].Title);
            Assert.AreEqual(1, model.Collection[0].Collection.Count);
            Assert.AreEqual("Tigger", model.Collection[0].Collection[0].Name);
            Assert.AreEqual("Female", model.Collection[1].Title);
            Assert.AreEqual(1, model.Collection[1].Collection.Count);
            Assert.AreEqual("Jerry", model.Collection[1].Collection[0].Name);
        }

        [TestMethod]
        public void CreatePetGroupsWithTwoOwnersSameGenderWithACat()
        {
            var factory = new ModelFactory();
            var testOwner = new Owner { Gender = "Male" };
            testOwner.Pets.Add(new Pet { Name = "Tigger", Type = "Cat" });
            var testOwner1 = new Owner { Gender = "Male" };
            testOwner1.Pets.Add(new Pet { Name = "Jerry", Type = "Cat" });
            var results = new List<Owner> { testOwner, testOwner1 };
            var model = factory.Create(results.GroupBy(owner => owner.Gender, owner => owner.Cats));

            Assert.IsNotNull(model);
            Assert.IsTrue(model.HasGroups);
            Assert.AreEqual(1, model.Collection.Count);
            Assert.AreEqual("Male", model.Collection[0].Title);
            Assert.AreEqual(2, model.Collection[0].Collection.Count);
            Assert.AreEqual("Jerry", model.Collection[0].Collection[0].Name);
            Assert.AreEqual("Tigger", model.Collection[0].Collection[1].Name);
        }

        [TestMethod]
        public void CreateOwnerListWithNoData()
        {
            var factory = new ModelFactory();
            var model = factory.Create(new List<Owner>());
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public void CreateOwnerListWithOneOwner()
        {
            var factory = new ModelFactory();
            var model = factory.Create(new List<Owner> { new Owner { Name = "John", Age = 44, Gender = "Male" } });
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual("John", model[0].Name);
        }

        [TestMethod]
        public void CreateOwnerListWithTwoOwners()
        {
            var factory = new ModelFactory();
            var model = factory.Create(new List<Owner>
            {
                new Owner {Name = "John", Age = 44, Gender = "Male"},
                new Owner {Name = "Sarah", Age = 45, Gender = "Female"}
            });
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual("John", model[0].Name);
            Assert.AreEqual("Sarah", model[1].Name);
        }

        [TestMethod]
        public void CreateOwnerWithNoData()
        {
            var factory = new ModelFactory();
            Owner owner = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var model = factory.Create(owner);
            Assert.IsNull(model);
        }

        [TestMethod]
        public void CreateOwnerWithData()
        {
            var factory = new ModelFactory();
            var model = factory.Create(new Owner { Name = "John", Age = 44, Gender = "Male" });
            Assert.IsNotNull(model);
            Assert.AreEqual("John", model.Name);
            Assert.IsNotNull(model.Pets);
            Assert.AreEqual(0, model.Pets.Collection.Count);
        }
    }
}
