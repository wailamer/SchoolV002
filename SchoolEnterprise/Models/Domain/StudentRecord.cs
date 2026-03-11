namespace SchoolEnterprise.Models.Domain;

public class StudentRecord
{
    public int StudentRecordId { get; set; }
    public int StudentId { get; set; }
    public string AcademicYear { get; set; } = string.Empty;
    public int ClassRoomId { get; set; }
    public int RollNumber { get; set; }
    public RecordStatus Status { get; set; } = RecordStatus.Active;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
