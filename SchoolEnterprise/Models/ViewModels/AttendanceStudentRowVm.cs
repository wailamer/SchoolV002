namespace SchoolEnterprise.Models.ViewModels;

public class AttendanceStudentRowVm
{
    public int StudentRecordId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public bool IsPresent { get; set; } = true;
    public string Notes { get; set; } = string.Empty;
}
