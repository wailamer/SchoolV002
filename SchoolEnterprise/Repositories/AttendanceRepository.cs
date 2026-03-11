using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class AttendanceRepository(XmlStorageService xml) : BaseXmlRepository<AttendanceEntry>(xml, "attendance.xml")
{
    public List<AttendanceEntry> GetAll() => Read();
    public List<AttendanceEntry> GetByRecord(int recordId) => Read().Where(x => x.StudentRecordId == recordId).ToList();
    public void Upsert(AttendanceEntry entity)
    {
        var list = Read();
        var i = list.FindIndex(x => x.StudentRecordId == entity.StudentRecordId && x.Date.Date == entity.Date.Date);
        if (i >= 0) list[i] = entity; else list.Add(entity);
        Write(list);
    }
}
