using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Customer:BaseEntity
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerGender { get; set; }
        public DateTime CustomerDOB { get; set; }
        public string CustomerEmail { get; set; }

        public List<Addresses> Addresses { get; set; } = new List<Addresses>();
    }
}
