namespace SchoolEnterprise.Models.ViewModels;

public class AttendanceBoardVm
{
    public string Grade { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Today;
    public List<AttendanceStudentRowVm> Students { get; set; } = [];
}
