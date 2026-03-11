namespace SchoolEnterprise.Models.ViewModels;

public class PrimaryReportVm
{
    public string StudentName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string MotherName { get; set; } = string.Empty;
    public string GradeSection { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public decimal FirstSemester { get; set; }
    public decimal SecondSemester { get; set; }
    public decimal Total { get; set; }
    public decimal Percentage { get; set; }
    public string FinalResult { get; set; } = string.Empty;
}
