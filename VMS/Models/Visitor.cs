namespace VMS.Models;
public partial class Visitor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public int PurposeId { get; set; }

    public string HostName { get; set; }

    public byte[] Photo { get; set; }

    public DateTime VisitDate { get; set; }

    public string FormSubmissionMode { get; set; }
    public int? VisitorPassCode { get; set; }

    public DateTime? CheckInTime { get; set; }

    public int? CheckedInBy { get; set; }
    public DateTime? CheckOutTime { get; set; }
    
    public int? CheckedOutBy { get; set; }
    public int OfficeLocationId { get; set; }

    public int? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual OfficeLocation? OfficeLocation { get; set; }

    public virtual PurposeOfVisit? Purpose { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual User? CheckedInByNavigation { get; set; }
    public virtual ICollection<VisitorDevice> VisitorDevices { get; set; } = new List<VisitorDevice>();
}
