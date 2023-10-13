namespace Weinmann.Domain.DTOs.Employee
{
    public class UpdateEmployeeDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int[] BusinessLocationIds { get; set; }
    }
}
