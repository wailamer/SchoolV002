namespace SchoolEnterprise.Models.Domain;

public class Teacher
{
    public int TeacherId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
