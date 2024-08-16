namespace VMS.Models.DTO
{
    public class UpdateUserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int OfficeLocationId { get; set; }
        public int IsActive { get; set; }
        public int RoleId { get; set; }
        public string loginUserName { get; set; }
        public DateTime? ValidFrom { get; set; }
    }
}
