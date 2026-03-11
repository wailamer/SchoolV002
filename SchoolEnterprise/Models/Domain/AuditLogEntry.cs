namespace SchoolEnterprise.Models.Domain;

public class AuditLogEntry
{
    public int AuditLogId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; } = string.Empty;
}
