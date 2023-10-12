using System.ComponentModel.DataAnnotations;

namespace Weinmann.Domain.Models
{
    public class Customer : BaseEntity
    {
        public string UserName { get; set; }

        public int BusinessLocationId { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public BusinessLocation BusinessLocation { get; set; }
    }
}
