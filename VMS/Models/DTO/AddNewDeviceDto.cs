using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class AddNewDeviceDto
    {
        [Required]
        public string? deviceName { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public int UpdatedBy { get; set; }
    }
}
