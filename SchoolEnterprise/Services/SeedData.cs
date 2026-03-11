using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class SeedData
{
    private readonly UserRepository _users;
    private readonly SubjectRepository _subjects;
    private readonly TeacherRepository _teachers;
    private readonly ClassRepository _classes;
    private readonly StudentRepository _students;
    private readonly StudentRecordRepository _records;
    private readonly SchoolSettingsRepository _settings;
    private readonly SequenceService _seq;

    public SeedData(UserRepository users, SubjectRepository subjects, TeacherRepository teachers, ClassRepository classes, StudentRepository students, StudentRecordRepository records, SchoolSettingsRepository settings, SequenceService seq)
    {
        _users = users; _subjects = subjects; _teachers = teachers; _classes = classes; _students = students; _records = records; _settings = settings; _seq = seq;
    }

    public void Initialize()
    {
        if (!_users.GetAll().Any())
        {
            _users.Add(new AppUser { UserId = _seq.Next("UserId"), Username = "admin", Password = "admin", Role = "Admin" });
            _users.Add(new AppUser { UserId = _seq.Next("UserId"), Username = "secretary", Password = "secretary", Role = "Secretary" });
            _users.Add(new AppUser { UserId = _seq.Next("UserId"), Username = "teacher", Password = "teacher", Role = "Teacher" });
        }

        if (!_teachers.GetAll().Any())
            _teachers.Add(new Teacher { TeacherId = _seq.Next("TeacherId"), FullName = "معلّم افتراضي", Specialization = "عام" });

        if (!_subjects.GetAll().Any())
            _subjects.Add(new Subject { SubjectId = _seq.Next("SubjectId"), Name = "اللغة العربية", TeacherId = _teachers.GetAll().First().TeacherId });

        if (!_classes.GetAll().Any())
            _classes.Add(new ClassRoom { ClassRoomId = _seq.Next("ClassId"), Grade = "الصف الأول", Section = "A", AcademicYear = "2025/2026" });

        if (!_students.GetAll().Any())
        {
            var st = new StudentRegistration { StudentId = _seq.Next("StudentId"), GeneralRegisterNumber = _seq.Next("GeneralRegister"), MinistryCode = "M-1001", FirstName = "أحمد", FatherName = "محمد", LastName = "الحموي", MotherName = "سعاد", BirthDate = DateTime.Today.AddYears(-8) };
            _students.Add(st);
            _records.Add(new StudentRecord { StudentRecordId = _seq.Next("StudentRecordId"), StudentId = st.StudentId, AcademicYear = "2025/2026", Grade = "الصف الأول", Section = "A", ClassRoomId = _classes.GetAll().First().ClassRoomId, RollNumber = 1, IsCurrent = true });
        }

        if (_settings.Get() is null)
            _settings.Save(new SchoolSettings { SchoolName = "مدرسة المؤسسة", CurrentAcademicYear = "2025/2026", Address = "دمشق", Phone = "000000" });
    }
}
