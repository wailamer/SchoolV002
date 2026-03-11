using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class TeacherRepository(XmlStorageService xml) : BaseXmlRepository<Teacher>(xml, "teachers.xml")
{
    public List<Teacher> GetAll() => Read();
    public Teacher? GetById(int id) => Read().FirstOrDefault(x => x.TeacherId == id);
    public void Add(Teacher entity) { var list = Read(); list.Add(entity); Write(list); }
    public void Update(Teacher entity) { var list = Read(); var i = list.FindIndex(x => x.TeacherId == entity.TeacherId); if (i >= 0) { list[i] = entity; Write(list);} }
    public void Delete(int id) { var list = Read(); list.RemoveAll(x => x.TeacherId == id); Write(list); }
}
