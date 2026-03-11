namespace SchoolEnterprise.Models.Domain;

public class StudentRecord
{
    public int StudentRecordId { get; set; }
    public int StudentId { get; set; }
    public string AcademicYear { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public int ClassRoomId { get; set; }
    public int RollNumber { get; set; }
    public bool IsCurrent { get; set; } = true;
    public bool IsArchived { get; set; }
}
