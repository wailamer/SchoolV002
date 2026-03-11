using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class SeedData(
    UserRepository users,
    TeacherRepository teachers,
    SubjectRepository subjects,
    ClassRepository classes,
    StudentRepository students,
    StudentRecordRepository records,
    SchoolSettingsRepository settings,
    SequenceService seq)
{
    public void EnsureSeeded()
    {
        if (!users.GetAll().Any())
        {
            users.Add(new AppUser { UserId = seq.Next("UserId"), Username = "admin", Password = "admin123", FullName = "System Admin", Role = UserRole.Admin });
            users.Add(new AppUser { UserId = seq.Next("UserId"), Username = "secretary", Password = "secret123", FullName = "School Secretary", Role = UserRole.Secretary });
            users.Add(new AppUser { UserId = seq.Next("UserId"), Username = "teacher", Password = "teach123", FullName = "Main Teacher", Role = UserRole.Teacher });
        }
        if (!teachers.GetAll().Any()) teachers.Add(new Teacher { TeacherId = seq.Next("TeacherId"), FullName = "أحمد المدرس", Phone = "09999", Specialty = "رياضيات" });
        if (!subjects.GetAll().Any()) subjects.Add(new Subject { SubjectId = seq.Next("SubjectId"), Name = "الرياضيات", TeacherId = teachers.GetAll().First().TeacherId });
        if (!classes.GetAll().Any()) classes.Add(new ClassRoom { ClassRoomId = seq.Next("ClassId"), Grade = "الخامس", Section = "A" });
        if (!students.GetAll().Any())
        {
            var s = new StudentRegistration { StudentId = seq.Next("StudentId"), GeneralRegisterNumber = $"GR-{seq.Next("GeneralRegister")}", MinistryCode = "M-1001", FirstName = "محمد", FatherName = "خالد", GrandFatherName = "سليم", LastName = "الحسن", MotherName = "ليلى", BirthDate = new DateTime(2014,1,1), Phone = "09000", Address = "دمشق" };
            students.Add(s);
            records.Add(new StudentRecord { StudentRecordId = seq.Next("StudentRecordId"), StudentId = s.StudentId, AcademicYear = "2025-2026", ClassRoomId = classes.GetAll().First().ClassRoomId, RollNumber = 1 });
        }
        if (settings.Get() is null) settings.Save(new SchoolSettings());
    }
}
