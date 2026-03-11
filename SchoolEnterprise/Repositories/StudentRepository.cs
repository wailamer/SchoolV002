using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class StudentRepository(XmlStorageService storage) : BaseXmlRepository<StudentRegistration>(storage)
{
    protected override string FileName => "students.xml";
    public StudentRegistration? GetById(int id) => GetAll().FirstOrDefault(x => x.StudentId == id);
    public StudentRegistration? GetByMinistryCode(string code) => GetAll().FirstOrDefault(x => x.MinistryCode == code);
    public void Add(StudentRegistration item) { var all = GetAll(); all.Add(item); SaveAll(all); }
    public void Update(StudentRegistration item) { var all = GetAll(); var i = all.FindIndex(x => x.StudentId == item.StudentId); if (i >= 0) { all[i] = item; SaveAll(all);} }
    public void Delete(int id) { var all = GetAll(); all.RemoveAll(x => x.StudentId == id); SaveAll(all); }
}

