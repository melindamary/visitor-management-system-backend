namespace VMS.Models.DTO
{
    public class VisitorDetailsDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime VisitDate { get; set; }
        public string OfficeLocation { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string VisitPurpose { get; set; }
        public string HostName { get; set; }

        public string Photo { get; set; }
        public int DeviceCount { get; set; }
        public List<DeviceDetailsDTO> DevicesCarried { get; set; }


    }
}
