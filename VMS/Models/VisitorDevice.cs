using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class VisitorDevice
{
    public int Id { get; set; }

    public int VisitorId { get; set; }

    public int DeviceId { get; set; }

    public string? SerialNumber { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Device Device { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual Visitor Visitor { get; set; } = null!;
}
