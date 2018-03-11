using Pets.DAL;
using Pets.WebSite.Models;
using System.Linq;

namespace Pets.WebSite.Pages
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IOwnerRepository ownerRepository) : base (ownerRepository) {}

        public PetGroups GroupOfPets { get; set; }

        public void OnGet()
        {
            var owners = OwnerRepository.GetAllOwners();

            if (owners != null)
            {
                GroupOfPets = ModelFactory.Create(owners.CatOwners.GroupBy(owner => owner.Gender, owner => owner.Cats));
                GroupOfPets.GroupedBy = "Gender";
            }
            else
            {
                GroupOfPets = new PetGroups();
            }
        }
    }
}
