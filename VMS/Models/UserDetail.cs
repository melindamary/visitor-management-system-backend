using System;
using System.Collections.Generic;

namespace VMS;

public partial class UserDetail
{
    public int UserDetailsId { get; set; }

    public int? UserId { get; set; }

    public int? OfficeLocationId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual OfficeLocation? OfficeLocation { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual User? User { get; set; }
}
