using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Img { get; set; }
        public double Price { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
