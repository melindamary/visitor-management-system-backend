using System;
using System.Collections.Generic;

namespace VMS;

public partial class PurposeOfVisit
{
    public int PurposeId { get; set; }

    public string? PurposeName { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual ICollection<Visitor> Visitors { get; set; } = new List<Visitor>();
}
