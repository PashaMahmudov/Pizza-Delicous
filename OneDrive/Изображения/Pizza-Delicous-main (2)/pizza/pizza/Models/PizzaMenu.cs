using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class PizzaMenu
    {
        public int Id { get; set; }
       

      
        public string Name { get; set; }         

        public string Description { get; set; }   

        [Required]
        public decimal Price { get; set; }       
        public string? ImageUrl { get; set; }     
        public bool IsActive { get; set; } = true;
        [NotMapped]
        public IFormFile? formFile { get; set; }
    }
}
