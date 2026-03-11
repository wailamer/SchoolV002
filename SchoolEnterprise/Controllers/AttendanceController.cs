using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin", "Teacher")]
public class AttendanceController(StudentRecordRepository records, StudentRepository students, AttendanceRepository attendance, SequenceService seq) : Controller
{
    public IActionResult Board(string grade = "الأول", string section = "A")
    {
        var vm = new AttendanceBoardVm { Grade = grade, Section = section, Date = DateTime.Today };
        vm.Students = records.GetByClass(grade, section).Select(r => new AttendanceStudentRowVm { StudentRecordId = r.StudentRecordId, StudentName = students.GetById(r.StudentId)?.FirstName ?? "-" }).ToList();
        return View(vm);
    }

    [HttpPost]
    public IActionResult Save(AttendanceBoardVm vm)
    {
        foreach (var s in vm.Students)
            attendance.Upsert(new AttendanceEntry { AttendanceId = seq.Next("AttendanceId"), StudentRecordId = s.StudentRecordId, Date = vm.Date, IsPresent = s.IsPresent, Notes = s.Notes });
        return RedirectToAction(nameof(Board), new { vm.Grade, vm.Section });
    }
}
