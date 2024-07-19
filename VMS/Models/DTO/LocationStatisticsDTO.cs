// File: Models/DTO/LocationStatisticsDTO.cs
namespace VMS.Models.DTO
{
    public class LocationStatisticsDTO
    {
        public string Location { get; set; }
        public int NumberOfSecurity { get; set; }
        public int PassesGenerated { get; set; }
        public int TotalVisitors { get; set; }
    }
}