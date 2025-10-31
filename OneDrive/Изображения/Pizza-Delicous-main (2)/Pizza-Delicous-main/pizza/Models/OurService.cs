using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace pizza.Models
{
    public class OurService
    {
        internal object formFile;

        public int Id { get; set; }              
        public string Icon { get; set; }        
        public string Title { get; set; }        
        public string Description { get; set; } 

        [NotMapped]
        public IFormFile? FormFile { get; set; }  

        public bool IsActive { get; set; } = true;
        public string Img { get; internal set; }
    }
}
