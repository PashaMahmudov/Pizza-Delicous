using System.ComponentModel.DataAnnotations;

namespace pizza.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }
        public string LastName {  get; set; }
        public string FirstName { get; set; }

        [Required, MaxLength(500)]
        public string Message { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;


     }
}
