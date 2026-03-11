using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class UserRepository(XmlStorageService xml) : BaseXmlRepository<AppUser>(xml, "users.xml")
{
    public List<AppUser> GetAll() => Read();
    public AppUser? GetById(int id) => Read().FirstOrDefault(x => x.UserId == id);
    public void Add(AppUser entity) { var list = Read(); list.Add(entity); Write(list); }
    public void Update(AppUser entity) { var list = Read(); var i = list.FindIndex(x => x.UserId == entity.UserId); if (i >= 0) { list[i] = entity; Write(list);} }
    public void Delete(int id) { var list = Read(); list.RemoveAll(x => x.UserId == id); Write(list); }
}
