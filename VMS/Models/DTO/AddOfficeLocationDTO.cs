using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class AddOfficeLocationDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Username { get; set; }
    }
}
