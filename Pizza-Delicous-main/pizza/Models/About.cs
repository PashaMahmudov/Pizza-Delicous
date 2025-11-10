using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class About

    {
        public int Id { get; set; }
        public string Img { get; set; }
     
        public string Title { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public IFormFile formFile { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
