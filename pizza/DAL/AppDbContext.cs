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
        public object Services { get; internal set; }
        public DbSet<PizzaMenu> PizzaMenus { get; set; }
        
        public DbSet<OurMenu> OurMenus { get; set; }


    }
}
