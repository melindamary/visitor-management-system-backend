using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class AddNewDeviceDTO
    {
        [Required]
        public string? deviceName { get; set; }

    }
}
