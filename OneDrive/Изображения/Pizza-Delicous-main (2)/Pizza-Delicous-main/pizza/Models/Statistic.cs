namespace pizza.Models
{
    public class Statistic
    {
        public int Id { get; set; }             
        public string Icon { get; set; }         
        public int Number { get; set; }         
        public string Title { get; set; }        
        public bool IsActive { get; set; } = true; 
    }
}
