using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class Slider
    {
        public int Id { get; set; }  
        public string Subheading { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Img { get; set; }
        public bool IsActive   { get; set; } = true;
        public bool Status  { get; set; }=true;

        [NotMapped]
        public IFormFile? formFile { get; set; }
    }
}
