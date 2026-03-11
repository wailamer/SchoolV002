using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class StudentRepository(XmlStorageService xml) : BaseXmlRepository<StudentRegistration>(xml, "students.xml")
{
    public List<StudentRegistration> GetAll() => Read();
    public StudentRegistration? GetById(int id) => Read().FirstOrDefault(x => x.StudentId == id);
    public StudentRegistration? GetByMinistryCode(string code) => Read().FirstOrDefault(x => x.MinistryCode == code);
    public void Add(StudentRegistration entity) { var list = Read(); list.Add(entity); Write(list); }
    public void Update(StudentRegistration entity) { var list = Read(); var i = list.FindIndex(x => x.StudentId == entity.StudentId); if (i >= 0) { list[i] = entity; Write(list);} }
    public void Delete(int id) { var list = Read(); list.RemoveAll(x => x.StudentId == id); Write(list); }
}
