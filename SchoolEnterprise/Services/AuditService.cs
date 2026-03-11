using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class AuditService(AuditLogRepository logs, SequenceService seq)
{
    public void Log(string action, string user, string description)
    {
        logs.Add(new AuditLogEntry
        {
            AuditLogId = seq.Next("AuditLogId"),
            Action = action,
            UserName = user,
            Description = description,
            CreatedAt = DateTime.Now
        });
    }
}
