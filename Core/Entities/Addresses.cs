namespace Core.Entities
{
    public class Addresses:BaseEntity
    {
        public string Address { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } 
    }
}
