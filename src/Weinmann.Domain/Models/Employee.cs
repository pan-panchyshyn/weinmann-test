namespace Weinmann.Domain.Models
{
    public class Employee : BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<EmployeeBusinessLocation> EmployeeBusinessLocations { get; set; }
    }
}
