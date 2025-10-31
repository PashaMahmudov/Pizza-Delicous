using pizza.Models;

namespace pizza.ViewModels
{
    public class HomeVM
    {

        public List<Slider> Sliders { get; set; }
        public List<Menu> Menus { get; set; }
        public Contact Contact { get; set; }
        public ContactMessage? ContactMessage { get; set; }
        public List<BlogPost>? BlogPosts { get; set; }
    }
}
