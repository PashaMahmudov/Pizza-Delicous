using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace pizza.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }

        [Required]
        public string Category { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
