using Microsoft.AspNetCore.Mvc.RazorPages;
using Pets.DAL;
using Pets.WebSite.Models;

namespace Pets.WebSite
{
    public class BasePageModel : PageModel
    {
        public BasePageModel(IOwnerRepository ownerRepository)
        {
            OwnerRepository = ownerRepository;
            ModelFactory = new ModelFactory();
        }

        public IOwnerRepository OwnerRepository { get; private set; }
        public ModelFactory ModelFactory { get; private set; }
    }
}
