namespace VMS.Models.DTO
{
    public class AddNewUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateOnly? ValidFrom { get; set; }


    }
}
