using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public abstract class XmlRepositoryBase<T>(XmlStorageService storage, string fileName)
{
    protected readonly XmlStorageService Storage = storage;
    protected readonly string FileName = fileName;
    public virtual List<T> GetAll() => Storage.LoadList<T>(FileName);
    public void SaveAll(List<T> rows) => Storage.SaveList(FileName, rows);
}

public class StudentRepository(XmlStorageService storage) : XmlRepositoryBase<StudentRegistration>(storage, "students.xml")
{
    public StudentRegistration? GetById(int id) => GetAll().FirstOrDefault(x => x.StudentId == id);
    public StudentRegistration? GetByMinistryCode(string code) => GetAll().FirstOrDefault(x => x.MinistryCode == code);
    public void Add(StudentRegistration row) { var d = GetAll(); d.Add(row); SaveAll(d); }
    public void Update(StudentRegistration row) { var d = GetAll(); var i = d.FindIndex(x => x.StudentId == row.StudentId); if (i >= 0) d[i] = row; SaveAll(d); }
    public void Delete(int id) { var d = GetAll(); d.RemoveAll(x => x.StudentId == id); SaveAll(d); }
}

public class StudentRecordRepository(XmlStorageService storage) : XmlRepositoryBase<StudentRecord>(storage, "student-records.xml")
{
    public StudentRecord? GetById(int id) => GetAll().FirstOrDefault(x => x.StudentRecordId == id);
    public StudentRecord? GetCurrentByStudentId(int studentId) => GetAll().Where(x => x.StudentId == studentId).OrderByDescending(x => x.StudentRecordId).FirstOrDefault();
    public List<StudentRecord> GetByClass(int classId) => GetAll().Where(x => x.ClassRoomId == classId && x.Status == RecordStatus.Active).ToList();
    public void Add(StudentRecord row) { var d = GetAll(); d.Add(row); SaveAll(d); }
    public void Update(StudentRecord row) { var d = GetAll(); var i = d.FindIndex(x => x.StudentRecordId == row.StudentRecordId); if (i >= 0) d[i] = row; SaveAll(d); }
    public void Delete(int id) { var d = GetAll(); d.RemoveAll(x => x.StudentRecordId == id); SaveAll(d); }
}

public class TeacherRepository(XmlStorageService storage) : XmlRepositoryBase<Teacher>(storage, "teachers.xml")
{
    public Teacher? GetById(int id) => GetAll().FirstOrDefault(x => x.TeacherId == id);
    public void Add(Teacher row) { var d = GetAll(); d.Add(row); SaveAll(d); }
    public void Update(Teacher row) { var d = GetAll(); var i = d.FindIndex(x => x.TeacherId == row.TeacherId); if (i >= 0) d[i] = row; SaveAll(d); }
    public void Delete(int id) { var d = GetAll(); d.RemoveAll(x => x.TeacherId == id); SaveAll(d); }
}
public class SubjectRepository(XmlStorageService storage) : XmlRepositoryBase<Subject>(storage, "subjects.xml")
{
    public Subject? GetById(int id) => GetAll().FirstOrDefault(x => x.SubjectId == id);
    public void Add(Subject row) { var d = GetAll(); d.Add(row); SaveAll(d); }
    public void Update(Subject row) { var d = GetAll(); var i = d.FindIndex(x => x.SubjectId == row.SubjectId); if (i >= 0) d[i] = row; SaveAll(d); }
    public void Delete(int id) { var d = GetAll(); d.RemoveAll(x => x.SubjectId == id); SaveAll(d); }
}
public class ClassRepository(XmlStorageService storage) : XmlRepositoryBase<ClassRoom>(storage, "classes.xml")
{
    public ClassRoom? GetById(int id) => GetAll().FirstOrDefault(x => x.ClassRoomId == id);
    public void Add(ClassRoom row) { var d = GetAll(); d.Add(row); SaveAll(d); }
    public void Update(ClassRoom row) { var d = GetAll(); var i = d.FindIndex(x => x.ClassRoomId == row.ClassRoomId); if (i >= 0) d[i] = row; SaveAll(d); }
    public void Delete(int id) { var d = GetAll(); d.RemoveAll(x => x.ClassRoomId == id); SaveAll(d); }
}
public class AttendanceRepository(XmlStorageService storage) : XmlRepositoryBase<AttendanceEntry>(storage, "attendance.xml")
{
    public List<AttendanceEntry> GetByRecord(int recordId) => GetAll().Where(x => x.StudentRecordId == recordId).ToList();
    public void Upsert(AttendanceEntry row)
    {
        var d = GetAll();
        var i = d.FindIndex(x => x.StudentRecordId == row.StudentRecordId && x.Date.Date == row.Date.Date);
        if (i >= 0) d[i] = row; else d.Add(row);
        SaveAll(d);
    }
}
public class MarkRepository(XmlStorageService storage) : XmlRepositoryBase<StudentMark>(storage, "marks.xml")
{
    public List<StudentMark> GetByRecordSemester(int recordId, int semester) => GetAll().Where(x => x.StudentRecordId == recordId && x.Semester == semester).ToList();
    public void Upsert(StudentMark row)
    {
        var d = GetAll();
        var i = d.FindIndex(x => x.StudentRecordId == row.StudentRecordId && x.SubjectId == row.SubjectId && x.Semester == row.Semester);
        if (i >= 0) d[i] = row; else d.Add(row);
        SaveAll(d);
    }
}
public class SchoolSettingsRepository(XmlStorageService storage) : XmlRepositoryBase<SchoolSettings>(storage, "school-settings.xml")
{
    public SchoolSettings? Get() => GetAll().FirstOrDefault();
    public void Save(SchoolSettings row) => SaveAll([row]);
}
public class UserRepository(XmlStorageService storage) : XmlRepositoryBase<AppUser>(storage, "users.xml")
{
    public AppUser? GetById(int id) => GetAll().FirstOrDefault(x => x.UserId == id);
    public void Add(AppUser row) { var d = GetAll(); d.Add(row); SaveAll(d); }
    public void Update(AppUser row) { var d = GetAll(); var i = d.FindIndex(x => x.UserId == row.UserId); if (i >= 0) d[i] = row; SaveAll(d); }
    public void Delete(int id) { var d = GetAll(); d.RemoveAll(x => x.UserId == id); SaveAll(d); }
}
public class AuditLogRepository(XmlStorageService storage) : XmlRepositoryBase<AuditLogEntry>(storage, "audit-log.xml")
{
    public void Add(AuditLogEntry row) { var d = GetAll(); d.Add(row); SaveAll(d); }
}
