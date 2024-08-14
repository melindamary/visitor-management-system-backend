namespace VMS.Models.DTO
{
    public class DeviceDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? Status { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
