namespace VMS.Models.DTO
{
    public class UserOverviewDTO
    {
        public int userId { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public string Location { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
