using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace pizza.Models
{
    public class OurService
    {
<<<<<<< HEAD
        internal object formFile;

        public int Id { get; set; }              
        public string Icon { get; set; }        
=======
      

        public int Id { get; set; }              
        public string? Img { get; set; }        
>>>>>>> 7813933 (Update)
        public string Title { get; set; }        
        public string Description { get; set; } 

        [NotMapped]
        public IFormFile? FormFile { get; set; }  

        public bool IsActive { get; set; } = true;
<<<<<<< HEAD
        public string Img { get; internal set; }
=======
    
>>>>>>> 7813933 (Update)
    }
}
