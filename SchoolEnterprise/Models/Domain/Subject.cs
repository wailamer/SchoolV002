namespace SchoolEnterprise.Models.Domain;

public class Subject
{
    public int SubjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public int TeacherId { get; set; }
}
