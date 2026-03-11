namespace SchoolEnterprise.Models.Domain;

public class StudentMark
{
    public int MarkId { get; set; }
    public int StudentRecordId { get; set; }
    public int SubjectId { get; set; }
    public int Semester { get; set; }
    public decimal Activities { get; set; }
    public decimal Oral { get; set; }
    public decimal Homework { get; set; }
    public decimal Exam { get; set; }
    public decimal Total { get; set; }
    public string Grade { get; set; } = string.Empty;
}
