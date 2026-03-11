using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class StudentRecordRepository(XmlStorageService xml) : BaseXmlRepository<StudentRecord>(xml, "student-records.xml")
{
    public List<StudentRecord> GetAll() => Read();
    public StudentRecord? GetById(int id) => Read().FirstOrDefault(x => x.StudentRecordId == id);
    public StudentRecord? GetCurrentByStudentId(int studentId) => Read().Where(x => x.StudentId == studentId && !x.IsArchived).OrderByDescending(x => x.StudentRecordId).FirstOrDefault();
    public List<StudentRecord> GetByClass(string grade, string section) => Read().Where(x => x.Grade == grade && x.Section == section && !x.IsArchived).ToList();
    public void Add(StudentRecord entity) { var list = Read(); list.Add(entity); Write(list); }
    public void Update(StudentRecord entity) { var list = Read(); var i = list.FindIndex(x => x.StudentRecordId == entity.StudentRecordId); if (i >= 0) { list[i] = entity; Write(list);} }
    public void Delete(int id) { var list = Read(); list.RemoveAll(x => x.StudentRecordId == id); Write(list); }
}
