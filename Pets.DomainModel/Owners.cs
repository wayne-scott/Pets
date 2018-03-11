using System.Collections.Generic;
using System.Linq;

namespace Pets.DomainModel
{
    /// <summary>
    /// Provide a filtered list of <see cref="Owner"/>.
    /// </summary>
    public class Owners
    {
        public Owners()
        {
            PetOwners = new List<Owner>();
        }

        public void Initialise(List<Owner> owners)
        {
            PetOwners = owners;
        }

        public List<Owner> PetOwners { get; private set; }

        public IQueryable<Owner> CatOwners => PetOwners?.Where(owner => owner.HasCats).AsQueryable();
    }
}
