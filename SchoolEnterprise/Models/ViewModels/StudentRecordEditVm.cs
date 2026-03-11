namespace SchoolEnterprise.Models.ViewModels;

public class StudentRecordEditVm
{
    public int StudentRecordId { get; set; }
    public string Grade { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public int RollNumber { get; set; }
}
