using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class Page
{
    public int PageId { get; set; }

    public string PageName { get; set; } = null!;

    public string PageUrl { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<PageControl> PageControls { get; set; } = new List<PageControl>();

    public virtual User UpdatedByNavigation { get; set; } = null!;
}
