namespace SchoolEnterprise.Models.ViewModels;

public class ClassReportRowVm
{
    public string StudentName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public decimal Percentage { get; set; }
    public string Result { get; set; } = string.Empty;
}
