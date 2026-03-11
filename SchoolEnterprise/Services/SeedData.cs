using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class SeedData(UserRepository users, SubjectRepository subjects, ClassRepository classes, StudentRepository students, StudentRecordRepository records, SchoolSettingsRepository settings, SequenceService seq)
{
    public void EnsureSeeded()
    {
        if (!users.GetAll().Any())
        {
            users.Add(new AppUser { UserId = seq.Next("UserId"), UserName = "admin", Password = "admin123", Role = "Admin" });
            users.Add(new AppUser { UserId = seq.Next("UserId"), UserName = "secretary", Password = "secret123", Role = "Secretary" });
            users.Add(new AppUser { UserId = seq.Next("UserId"), UserName = "teacher", Password = "teach123", Role = "Teacher" });
        }

        if (!subjects.GetAll().Any()) subjects.Add(new Subject { SubjectId = seq.Next("SubjectId"), Name = "الرياضيات", Grade = "الأول" });
        if (!classes.GetAll().Any()) classes.Add(new ClassRoom { ClassRoomId = seq.Next("ClassRoomId"), Grade = "الأول", Section = "A", AcademicYear = "2025-2026" });
        if (!students.GetAll().Any())
        {
            var s = new StudentRegistration
            {
                StudentId = seq.Next("StudentId"),
                GeneralRegisterNumber = seq.Next("GeneralRegister"),
                MinistryCode = "MIN-0001",
                FirstName = "محمد",
                FatherName = "أحمد",
                LastName = "خالد",
                MotherName = "سعاد",
                BirthDate = new DateTime(2017, 1, 1)
            };
            students.Add(s);
            records.Add(new StudentRecord { StudentRecordId = seq.Next("StudentRecordId"), StudentId = s.StudentId, AcademicYear = "2025-2026", Grade = "الأول", Section = "A", RollNumber = 1 });
        }

        if (settings.Get() is null)
        {
            settings.Save(new SchoolSettings());
        }
    }
}
