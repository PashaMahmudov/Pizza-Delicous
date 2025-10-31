using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class Footer
    {
        public int Id { get; set; }

        // About
        [Required]
        public string AboutText { get; set; }

        // Contact
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        // Socials 
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }

        // Blog postlar
        public string BlogTitle1 { get; set; }
        public string BlogImage1 { get; set; }
        public string BlogDate1 { get; set; }
        public string BlogAuthor1 { get; set; }
        public int BlogComment1 { get; set; }

        public string BlogTitle2 { get; set; }
        public string BlogImage2 { get; set; }
        public string BlogDate2 { get; set; }
        public string BlogAuthor2 { get; set; }
        public int BlogComment2 { get; set; }

        [NotMapped]
        public IFormFile? FormFile1 { get; set; }
        [NotMapped]
        public IFormFile? FormFile2 { get; set; }
    }
}
