using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class UserDetail
{
    public int UserDetailsId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
