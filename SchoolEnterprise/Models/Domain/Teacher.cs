namespace SchoolEnterprise.Models.Domain;

public class Teacher
{
    public int TeacherId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
}
