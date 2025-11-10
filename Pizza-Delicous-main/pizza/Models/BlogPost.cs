using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class BlogPost
    {
      
            public int Id { get; set; }

           
            public string Title { get; set; }

            public string Description { get; set; }

            public string? ImageUrl { get; set; }

            public DateTime Date { get; set; }

            public string Author { get; set; }

            public int CommentCount { get; set; }

            public string Slug { get; set; }

           
            public BlogDetail? BlogDetail { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
