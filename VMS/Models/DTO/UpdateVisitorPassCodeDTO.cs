using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class UpdateVisitorPassCodeDTO
    {
        [Required]
        public string? VisitorPassCode { get; set; }

    }
}
