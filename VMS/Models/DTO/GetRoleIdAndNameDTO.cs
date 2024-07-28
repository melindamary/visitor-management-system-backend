using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class GetRoleIdAndNameDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ?CreatedBy { get; set; }
        public DateTime ?CreatedDate { get; set; }
    }
}
