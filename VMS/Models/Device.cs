using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class Device
{
    public int DeviceId { get; set; }

    public string DeviceName { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual ICollection<VisitorDevice> VisitorDevices { get; set; } = new List<VisitorDevice>();
}
