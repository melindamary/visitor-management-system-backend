using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class GetDeviceIdAndNameDTO
    {
        [Required]
        public int DeviceId { get; set; }
        [Required]
        public string DeviceName { get; set; }
    }
}
