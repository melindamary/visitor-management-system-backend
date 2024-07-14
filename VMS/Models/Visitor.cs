using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public string VisitorName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int PurposeId { get; set; }

    public string HostName { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public DateOnly VisitDate { get; set; }

    public string? VisitorPassCode { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public int? UserId { get; set; }

    public int LocationId { get; set; }

    public int Status { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual OfficeLocation Location { get; set; } = null!;

    public virtual PurposeOfVisit Purpose { get; set; } = null!;

    public virtual User UpdatedByNavigation { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual ICollection<VisitorDevice> VisitorDevices { get; set; } = new List<VisitorDevice>();
}
