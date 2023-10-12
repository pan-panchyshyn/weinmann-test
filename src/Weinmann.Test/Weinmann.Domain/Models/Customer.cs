using System.ComponentModel.DataAnnotations;

namespace Weinmann.Domain.Models
{
    public class Customer : BaseEntity
    {
        public string UserName { get; private set; }

        [Required]
        public byte[] PasswordHash { get; private set; }

        [Required]
        public byte[] PasswordSalt { get; private set; }

        public Customer(string userName, byte[] passwordHash, byte[] passwordSalt)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
