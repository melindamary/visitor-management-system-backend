﻿using System;
using System.Collections.Generic;

namespace VMS.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public int UserDetailsId { get; set; }

    public int LocationId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public DateOnly ValidFrom { get; set; }

    public int IsActive { get; set; }

    public int IsLoggedIn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Device> DeviceCreatedByNavigations { get; set; } = new List<Device>();

    public virtual ICollection<Device> DeviceUpdatedByNavigations { get; set; } = new List<Device>();

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseUpdatedByNavigation { get; set; } = new List<User>();

    public virtual OfficeLocation Location { get; set; } = null!;

    public virtual ICollection<OfficeLocation> OfficeLocationCreatedByNavigations { get; set; } = new List<OfficeLocation>();

    public virtual ICollection<OfficeLocation> OfficeLocationUpdatedByNavigations { get; set; } = new List<OfficeLocation>();

    public virtual ICollection<PageControl> PageControlCreatedByNavigations { get; set; } = new List<PageControl>();

    public virtual ICollection<PageControl> PageControlUpdatedByNavigations { get; set; } = new List<PageControl>();

    public virtual ICollection<Page> PageCreatedByNavigations { get; set; } = new List<Page>();

    public virtual ICollection<Page> PageUpdatedByNavigations { get; set; } = new List<Page>();

    public virtual ICollection<PurposeOfVisit> PurposeOfVisitCreatedByNavigations { get; set; } = new List<PurposeOfVisit>();

    public virtual ICollection<PurposeOfVisit> PurposeOfVisitUpdatedByNavigations { get; set; } = new List<PurposeOfVisit>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Role> RoleCreatedByNavigations { get; set; } = new List<Role>();

    public virtual ICollection<Role> RoleUpdatedByNavigations { get; set; } = new List<Role>();

    public virtual User UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetailCreatedByNavigations { get; set; } = new List<UserDetail>();

    public virtual ICollection<UserDetail> UserDetailUpdatedByNavigations { get; set; } = new List<UserDetail>();

    public virtual UserDetail UserDetails { get; set; } = null!;

    public virtual ICollection<Visitor> VisitorCreatedByNavigations { get; set; } = new List<Visitor>();

    public virtual ICollection<VisitorDevice> VisitorDeviceCreatedByNavigations { get; set; } = new List<VisitorDevice>();

    public virtual ICollection<VisitorDevice> VisitorDeviceUpdatedByNavigations { get; set; } = new List<VisitorDevice>();

    public virtual ICollection<Visitor> VisitorUpdatedByNavigations { get; set; } = new List<Visitor>();

    public virtual ICollection<Visitor> VisitorUsers { get; set; } = new List<Visitor>();
}
