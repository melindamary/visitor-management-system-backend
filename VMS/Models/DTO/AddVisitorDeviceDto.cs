using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class AddVisitorDeviceDto
    {

        [Required]
        public int VisitorId { get; set; }
        // Visitor ID (foreign key)
        [Required]
        public int DeviceId { get; set; }           // Item ID (foreign key)
        [Required]
        public string? SerialNumber { get; set; }  // Serial number of the item
        }
    }



