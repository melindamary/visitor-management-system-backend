using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class GetLocationIdAndNameDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
