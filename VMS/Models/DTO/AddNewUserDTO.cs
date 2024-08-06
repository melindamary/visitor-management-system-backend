namespace VMS.Models.DTO
{
    public class AddNewUserDTO
    {
        // User properties
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? ValidFrom { get; set; }

        // UserDetail properties
        public int OfficeLocationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        // UserRole property
        public int RoleId { get; set; }

       



    }
}
