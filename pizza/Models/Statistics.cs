using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class Statistics
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // Pizza Branches, Number of Awards, etc.

        [Required]
        public int Number { get; set; } // 100, 85, 10567, 900

        public string? Icon { get; set; } // Icon fayl yolu / flaticon class

        public bool IsActive { get; set; } = true; // Aktiv / Deaktiv

        [NotMapped]
        public IFormFile? FormFile { get; set; } // Şəkil əlavə üçün
    }
}
