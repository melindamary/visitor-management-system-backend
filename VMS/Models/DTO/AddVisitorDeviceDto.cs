using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class AddVisitorDeviceDTO
    {

     
        public int VisitorId { get; set; }
        // Visitor ID (foreign key)
       
        public int DeviceId { get; set; }           // Item ID (foreign key)
       
        public string? SerialNumber { get; set; }  // Serial number of the item
        }
    }



