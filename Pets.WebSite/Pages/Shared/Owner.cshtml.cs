namespace Pets.WebSite.Pages.Shared
{
    public class Owner
    {
        public Owner()
        {
            Pets = new Pets();
        }

        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public Pets Pets { get; set; }
    }
}
