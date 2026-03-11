namespace SchoolEnterprise.Models.Domain;

public class ClassRoom
{
    public int ClassRoomId { get; set; }
    public string Grade { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
}
