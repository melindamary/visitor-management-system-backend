﻿using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class OfficeLocation
{
    public int OfficeLocationId { get; set; }

    public string? LocationName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();

    public virtual ICollection<UserLocation> UserLocations { get; set; } = new List<UserLocation>();

    public virtual ICollection<Visitor> Visitors { get; set; } = new List<Visitor>();
}
