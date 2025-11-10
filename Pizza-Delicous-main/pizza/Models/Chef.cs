using System.ComponentModel.DataAnnotations.Schema;

namespace pizza.Models
{
    public class Chef
    {
        public int Id { get; set; }
        public string Img {  get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
