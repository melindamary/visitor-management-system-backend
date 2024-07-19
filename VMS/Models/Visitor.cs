using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public string? VisitorName { get; set; }

    public string? Phone { get; set; }

    public int? PurposeId { get; set; }

    public string? HostName { get; set; }

    public byte[]? Photo { get; set; }

    public DateTime? VisitDate { get; set; }

    public string? VisitorPassCode { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public int? UserId { get; set; }

    public int? OfficeLocationId { get; set; }

    public int? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual OfficeLocation? OfficeLocation { get; set; }

    public virtual PurposeOfVisit? Purpose { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<VisitorDevice> VisitorDevices { get; set; } = new List<VisitorDevice>();
}
