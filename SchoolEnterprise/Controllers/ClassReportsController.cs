using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin", "Secretary", "Teacher")]
public class ClassReportsController(StudentRecordRepository records, StudentRepository students, MarkRepository marks) : Controller
{
    public IActionResult Index(string grade = "الأول", string section = "A")
    {
        var vm = records.GetByClass(grade, section).Select(r =>
        {
            var total = marks.GetAll().Where(m => m.StudentRecordId == r.StudentRecordId).Sum(m => m.Total);
            return new ClassReportRowVm { StudentName = students.GetById(r.StudentId)?.FirstName ?? "-", Total = total, Percentage = total / 2, Result = total >= 100 ? "ناجح" : "متابعة" };
        }).ToList();
        return View(vm);
    }
}
