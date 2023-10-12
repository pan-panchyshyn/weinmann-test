namespace Weinmann.Domain.DTOs.BusinessLocation
{
    public class BusinessLocationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<int> CustomerIds { get; set; }
    }
}
