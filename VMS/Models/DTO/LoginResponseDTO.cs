namespace VMS.Models.DTO
{
    public class LoginResponseDTO
    {
        public string? Username { get; set; }
        public string? Location { get; set; }

        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}
