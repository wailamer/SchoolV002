namespace SchoolEnterprise.Models.Domain;

public class StudentRecord
{
    public int StudentRecordId { get; set; }
    public int StudentId { get; set; }
    public string AcademicYear { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public int RollNumber { get; set; }
    public bool IsPromoted { get; set; }
    public bool IsArchived { get; set; }
}
