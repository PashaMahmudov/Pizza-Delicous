using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class OurMenu
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }          

        [Required]
        public string Description { get; set; }  

        [Required]
        public decimal Price { get; set; }      

        public string? ImageUrl { get; set; }    

        public bool IsActive { get; set; } = true;  

        [NotMapped]
        public IFormFile? FormFile { get; set; }     
    }
}
