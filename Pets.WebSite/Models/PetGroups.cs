using System.Collections.Generic;

namespace Pets.WebSite.Models
{
    public class PetGroups
    {
        public PetGroups() => Collection = new List<Pages.Shared.Pets>();

        public bool HasGroups => Collection.Count > 0;

        public IList<Pages.Shared.Pets> Collection { get; }

        public string GroupedBy { get; set; }
    }
}
