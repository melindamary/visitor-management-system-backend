using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class PageControl
{
    public int PageControlId { get; set; }

    public int RoleId { get; set; }

    public int PageId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Page Page { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User UpdatedByNavigation { get; set; } = null!;
}
