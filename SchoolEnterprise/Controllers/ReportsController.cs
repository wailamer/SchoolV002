using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin", "Secretary", "Teacher")]
public class ReportsController(StudentRepository students, StudentRecordRepository records, MarkRepository marks, SchoolSettingsRepository settings) : Controller
{
    public IActionResult PrimaryReport(int studentId)
    {
        var s = students.GetById(studentId); if (s is null) return NotFound();
        var r = records.GetCurrentByStudentId(studentId); if (r is null) return NotFound();
        var sem1 = marks.GetByRecordSemester(r.StudentRecordId, 1).Sum(x => x.Total);
        var sem2 = marks.GetByRecordSemester(r.StudentRecordId, 2).Sum(x => x.Total);
        var total = sem1 + sem2;
        var vm = new PrimaryReportVm { StudentName = $"{s.FirstName} {s.LastName}", FatherName = s.FatherName, MotherName = s.MotherName, GradeSection = $"{r.Grade}-{r.Section}", AcademicYear = r.AcademicYear, FirstSemester = sem1, SecondSemester = sem2, Total = total, Percentage = total / 2, FinalResult = total >= 100 ? "ناجح" : "يحتاج متابعة" };
        ViewBag.Settings = settings.Get();
        ViewBag.Student = s; ViewBag.Record = r;
        return View(vm);
    }

    public IActionResult MarkSheet(int recordId)
    {
        ViewBag.Marks = marks.GetAll().Where(x => x.StudentRecordId == recordId).ToList();
        return View();
    }
}
