using System;
using Pets.DAL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Pets.WebSite.Pages.Owner
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IOwnerRepository ownerRepository) : base (ownerRepository) { Message = string.Empty; }

        public IList<Shared.Owner> Owners { get; set; }
        public Shared.Owner Owner { get; set; }
        [TempData]
        public string Message { get; set; }

        public void OnGet(string ownerName)
        {
            if (!string.IsNullOrEmpty(ownerName))
            {
                var owners = OwnerRepository.GetAllOwners();
                if (owners?.PetOwners.Count > 0)
                {
                    Owner = ModelFactory.Create(owners.PetOwners.FirstOrDefault(owner => owner.Name == ownerName));
                }
                
                if (Owner == null)
                {
                    Message = $"{ownerName} not found?";
                }
            }
            else
            {
                var owners = OwnerRepository.GetAllOwners();
                if (owners?.PetOwners.Count > 0)
                {
                    Owners = ModelFactory.Create(owners.PetOwners);
                }

                if (Owners == null)
                {
                    Message = "No owners found?";
                }
            }
        }
    }
}