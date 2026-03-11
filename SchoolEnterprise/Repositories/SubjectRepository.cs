using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class SubjectRepository(XmlStorageService xml) : BaseXmlRepository<Subject>(xml, "subjects.xml")
{
    public List<Subject> GetAll() => Read();
    public Subject? GetById(int id) => Read().FirstOrDefault(x => x.SubjectId == id);
    public void Add(Subject entity) { var list = Read(); list.Add(entity); Write(list); }
    public void Update(Subject entity) { var list = Read(); var i = list.FindIndex(x => x.SubjectId == entity.SubjectId); if (i >= 0) { list[i] = entity; Write(list);} }
    public void Delete(int id) { var list = Read(); list.RemoveAll(x => x.SubjectId == id); Write(list); }
}
