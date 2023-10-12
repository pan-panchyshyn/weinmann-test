namespace Weinmann.Domain.Models
{
    public class BusinessLocation : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
