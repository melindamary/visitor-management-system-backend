using System.ComponentModel.DataAnnotations;

namespace VMS.Models.DTO
{
    public class UpdateVisitorPassCodeDTO
    {
        [Required]
        public int? VisitorPassCode { get; set; }
        public string Username { get; set; }


    }
}
