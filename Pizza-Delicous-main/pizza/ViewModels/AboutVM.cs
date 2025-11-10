using pizza.Models;

namespace pizza.ViewModels
{
    public class AboutVM
    {
        public Contact Contact { get; set; }
        public ContactMessage? ContactMessage { get; set; }
        public List<Chef> Chefs { get; set; }
        public ContactInfo ContactInfo { get; internal set; }
    }

}
