using Microsoft.AspNetCore.Mvc.RazorPages;
using Pets.DomainModel;
using System.Collections.Generic;

namespace Pets.WebSite.Pages.Shared
{
    public class Pets : PageModel
    {
        public Pets() => Collection = new List<Pet>();
        public string Title { get; set; }
        public List<Pet> Collection { get; internal set; }
    }
}
