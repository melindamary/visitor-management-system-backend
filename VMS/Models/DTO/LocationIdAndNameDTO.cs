using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class LocationIdAndNameDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
