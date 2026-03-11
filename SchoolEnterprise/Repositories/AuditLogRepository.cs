using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class AuditLogRepository(XmlStorageService xml) : BaseXmlRepository<AuditLogEntry>(xml, "audit-log.xml")
{
    public List<AuditLogEntry> GetAll() => Read().OrderByDescending(x => x.CreatedAt).ToList();
    public void Add(AuditLogEntry entity) { var list = Read(); list.Add(entity); Write(list); }
}
