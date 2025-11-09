using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class Meal
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
