using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class AuditService
{
    private readonly AuditLogRepository _repo;
    private readonly SequenceService _seq;
    private readonly IHttpContextAccessor _accessor;

    public AuditService(AuditLogRepository repo, SequenceService seq, IHttpContextAccessor accessor)
    {
        _repo = repo;
        _seq = seq;
        _accessor = accessor;
    }

    public void Log(string action, string details)
    {
        _repo.Add(new AuditLogEntry
        {
            AuditLogId = _seq.Next("AuditLogId"),
            Action = action,
            Details = details,
            Username = _accessor.HttpContext?.Session.GetString("username") ?? "system",
            CreatedAt = DateTime.UtcNow
        });
    }
}
