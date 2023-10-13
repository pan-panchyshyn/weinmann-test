namespace Weinmann.Domain.DTOs.Employee
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<int> BusinessLocationIds { get; set; }
        public List<int> CustomerIds { get; set; }
    }
}
