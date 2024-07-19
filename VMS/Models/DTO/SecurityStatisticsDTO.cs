namespace VMS.Models.DTO
{
    public class SecurityStatisticsDTO
    {
        public string Location { get; set; }
        public string SecurityFirstName { get; set; }
        public string SecurityLastName { get; set; }
        public string PhoneNumber { get; set; }
        public int? Status { get; set; }
        public int VisitorsApproved { get; set; }
    }
}
