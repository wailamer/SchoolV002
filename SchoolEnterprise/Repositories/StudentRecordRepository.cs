using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class StudentRecordRepository(XmlStorageService storage) : BaseXmlRepository<StudentRecord>(storage)
{
    protected override string FileName => "student-records.xml";
    public StudentRecord? GetById(int id) => GetAll().FirstOrDefault(x => x.StudentRecordId == id);
    public StudentRecord? GetCurrentByStudentId(int studentId) => GetAll().FirstOrDefault(x => x.StudentId == studentId && x.IsCurrent);
    public List<StudentRecord> GetByClass(int classId) => GetAll().Where(x => x.ClassRoomId == classId && !x.IsArchived).OrderBy(x => x.RollNumber).ToList();
    public void Add(StudentRecord item) { var all = GetAll(); all.Add(item); SaveAll(all); }
    public void Update(StudentRecord item) { var all = GetAll(); var i = all.FindIndex(x => x.StudentRecordId == item.StudentRecordId); if (i >= 0) { all[i] = item; SaveAll(all);} }
    public void Delete(int id) { var all = GetAll(); all.RemoveAll(x => x.StudentRecordId == id); SaveAll(all); }
}

