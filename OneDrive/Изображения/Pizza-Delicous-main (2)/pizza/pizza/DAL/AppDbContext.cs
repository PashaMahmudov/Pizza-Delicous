using Microsoft.EntityFrameworkCore;
using pizza.Models;
using pizza.ViewComponents;
using System.CodeDom;

namespace pizza.DAL
{
    public class ApplicationDbContext : DbContext
    {
        internal object blogPosts;

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
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Statistic> Statistics { get; set; }

        public DbSet<Category> categories{ get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<BlogPost> blogs { get; set; }
        public DbSet<BlogDetail> blogsDetail { get; set; }
        public DbSet<Comment> comments { get; set; }
        //public object BlogPosts { get; internal set; }
        public DbSet<ContactMessage>contactMessages  { get; set; }
    }
}
