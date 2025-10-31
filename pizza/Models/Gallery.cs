using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class Gallery
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }  

        [NotMapped]
        public IFormFile? FormFile { get; set; } 
    }
}
