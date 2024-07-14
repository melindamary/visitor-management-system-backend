using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class OfficeLocation
{
    public int LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Visitor> Visitors { get; set; } = new List<Visitor>();
}
