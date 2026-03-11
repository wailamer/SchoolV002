using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class AuditService(AuditLogRepository repo, SequenceService seq)
{
    public void Log(string action, string username, string details)
    {
        repo.Add(new AuditLogEntry
        {
            AuditLogId = seq.Next("AuditLogId"),
            Action = action,
            Username = username,
            Details = details,
            Timestamp = DateTime.UtcNow
        });
    }
}
