using System.ComponentModel.DataAnnotations;

namespace Api.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string CustomerFirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string CustomerLastName { get; set; }
        [Required]
        [MaxLength(1)]
        public string CustomerGender { get; set; }
        [Required]
        public DateTime CustomerDOB { get; set; }
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Required]
        public List<string> Addresses { get; set; } = new List<string>();
    }
}
