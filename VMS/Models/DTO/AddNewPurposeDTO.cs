using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class AddNewPurposeDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public int UpdatedBy { get; set; }
    }
}
