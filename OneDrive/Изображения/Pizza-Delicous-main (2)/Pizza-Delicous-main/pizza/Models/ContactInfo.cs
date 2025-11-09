namespace pizza.Models
{
    public class ContactInfo
    {
       
        
            public int Id { get; set; }

            public string Address { get; set; }
            public int Phone { get; set; }
            public string Email { get; set; }
            public string Website { get; set; }
            public bool IsActive { get; set; } = true;
        
    }

}

