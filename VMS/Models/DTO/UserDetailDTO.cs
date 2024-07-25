namespace VMS.Models.DTO
{
    public class UserDetailDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }
        public int? OfficeLocationId { get; set; }
       
        public int? IsActive { get; set; }
        public DateTime? ValidFrom { get; set; }
    }
}
