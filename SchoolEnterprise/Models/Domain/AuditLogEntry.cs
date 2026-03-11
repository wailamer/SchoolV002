namespace SchoolEnterprise.Models.Domain;

public class AuditLogEntry
{
    public int AuditLogId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Details { get; set; } = string.Empty;
}
