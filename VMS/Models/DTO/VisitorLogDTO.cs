namespace VMS.Models.DTO
{
    public class VisitorLogDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? PurposeName { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public int? VisitorPassCode { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? HostName { get; set; }
        public string? PhotoBase64 { get; set; }
        public byte[]? Photo { get; set; }
        public List<DeviceDetailsDTO> Devices { get; set; } = new List<DeviceDetailsDTO>(); // Initialize list to avoid null reference
    }

}
