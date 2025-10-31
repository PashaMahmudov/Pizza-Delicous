using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }
        public string? Img {  get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
