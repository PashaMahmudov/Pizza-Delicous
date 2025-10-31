using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class BlogDetail
    {
      
        public int Id { get; set; } 

        public string Title { get; set; } 
        public string Description { get; set; }
        public string Image { get; set; }

        [ForeignKey("BlogPost")]
        public int BlogId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}

