namespace SchoolEnterprise.Models.Domain;

public class SchoolSettings
{
    public int SchoolSettingsId { get; set; } = 1;
    public string SchoolName { get; set; } = "المدرسة النموذجية";
    public string PrincipalName { get; set; } = "مدير المدرسة";
    public string YearDefault { get; set; } = "2025-2026";
    public string GuidanceText { get; set; } = "المتابعة اليومية أساس النجاح.";
}
