using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class ClassRepository(XmlStorageService xml) : BaseXmlRepository<ClassRoom>(xml, "classes.xml")
{
    public List<ClassRoom> GetAll() => Read();
    public ClassRoom? GetById(int id) => Read().FirstOrDefault(x => x.ClassRoomId == id);
    public void Add(ClassRoom entity) { var list = Read(); list.Add(entity); Write(list); }
    public void Update(ClassRoom entity) { var list = Read(); var i = list.FindIndex(x => x.ClassRoomId == entity.ClassRoomId); if (i >= 0) { list[i] = entity; Write(list);} }
    public void Delete(int id) { var list = Read(); list.RemoveAll(x => x.ClassRoomId == id); Write(list); }
}
