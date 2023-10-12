namespace Weinmann.Domain.Models
{
    public class EmployeeBusinessLocation
    {
        public int EmployeeId { get; set; }
        public int BusinessLocationId { get; set; }

        public Employee Employee { get; set; }
        public BusinessLocation BusinessLocation { get; set; }
    }
}
