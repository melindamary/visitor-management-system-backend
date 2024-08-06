namespace VMS.Models.DTO
{
    public class VisitorReportDetailsDTO
    {
        public int VisitorId { get; set; }

        public string VisitorName { get; set; }

        public string Phone { get; set; }

        public DateTime VisitDate { get; set; }

        public string LocationName { get; set; }

        public string PurposeName { get; set; }
        public string HostName { get; set; }

        public string StaffName { get; set; }
        public string StaffPhoneNumber { get; set; }
        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public string Photo { get; set; }
        public int DeviceCount { get; set; }
        public List<DeviceDetailsDTO> Devices { get; set; }


    }
}
