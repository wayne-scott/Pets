using Pets.WebSite.Pages.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Pets.WebSite.Models
{
    public class ModelFactory
    {
        public PetGroups Create(IEnumerable<IGrouping<string, IEnumerable<DomainModel.Pet>>> groupedPets)
        {
            var model = new PetGroups();
            foreach (var groupAndPets in groupedPets)
            {
                var pets = new Pages.Shared.Pets { Title = groupAndPets.Key };
                foreach (var petsForGroup in groupAndPets)
                {
                    pets.Collection.AddRange(petsForGroup);
                }
                pets.Collection = pets.Collection.OrderBy(pet => pet.Name).ToList();
                model.Collection.Add(pets);
            }
            return model;
        }

        public Owner Create(DomainModel.Owner owner)
        {
            if (owner == null)
            {
                return null;
            }

            var modelOwner = new Owner
            {
                Name = owner.Name,
                Age = owner.Age,
                Gender = owner.Gender,
                Pets =
                {
                    Title = "Pets",
                    Collection = owner.Pets ?? new List<DomainModel.Pet>()
                }
            };
            return modelOwner;
        }

        public List<Owner> Create(IEnumerable<DomainModel.Owner> owners)
        {
            return owners.Select(owner => new Owner { Name = owner.Name }).ToList();
        }
    }
}
