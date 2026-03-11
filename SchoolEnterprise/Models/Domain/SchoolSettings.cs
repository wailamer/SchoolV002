namespace SchoolEnterprise.Models.Domain;

public class SchoolSettings
{
    public int SchoolSettingsId { get; set; } = 1;
    public string SchoolName { get; set; } = "مدرسة افتراضية";
    public string CurrentAcademicYear { get; set; } = "2025/2026";
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
