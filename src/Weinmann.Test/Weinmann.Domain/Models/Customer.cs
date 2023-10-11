using System.ComponentModel.DataAnnotations;

namespace Weinmann.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}
