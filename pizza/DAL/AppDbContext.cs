using Microsoft.EntityFrameworkCore;
using pizza.Models;
using pizza.ViewComponents;
using System.CodeDom;

namespace pizza.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public object Contact { get; internal set; }
        //public object About { get; internal set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet <OurService> OurServis { get; set; }
<<<<<<< HEAD
        public object Services { get; internal set; }
        public DbSet<PizzaMenu> PizzaMenus { get; set; }
        
        public DbSet<OurMenu> OurMenus { get; set; }
=======
        public DbSet<PizzaMenu> PizzaMenus { get; set; }
        
        public DbSet<OurMenu> OurMenus { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
>>>>>>> 7813933 (Update)


    }
}
