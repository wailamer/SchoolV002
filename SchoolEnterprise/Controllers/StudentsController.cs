using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin", "Secretary")]
public class StudentsController(StudentRepository students, StudentRecordRepository records, SequenceService seq, PromotionService promotion, AuditService audit) : Controller
{
    public IActionResult Index() => View(students.GetAll());

    public IActionResult Details(int id)
    {
        ViewBag.Record = records.GetCurrentByStudentId(id);
        return View(students.GetById(id));
    }

    public IActionResult Create() => View(new StudentCreateVm { AcademicYear = "2025-2026" });

    [HttpPost]
    public IActionResult Create(StudentCreateVm vm)
    {
        if (students.GetByMinistryCode(vm.MinistryCode) is not null) { ModelState.AddModelError("MinistryCode", "الكود الوزاري مستخدم"); return View(vm); }
        var s = new StudentRegistration { StudentId = seq.Next("StudentId"), GeneralRegisterNumber = seq.Next("GeneralRegister"), MinistryCode = vm.MinistryCode, FirstName = vm.FirstName, FatherName = vm.FatherName, LastName = vm.LastName };
        students.Add(s);
        records.Add(new StudentRecord { StudentRecordId = seq.Next("StudentRecordId"), StudentId = s.StudentId, AcademicYear = vm.AcademicYear, Grade = vm.Grade, Section = vm.Section, RollNumber = 1 });
        audit.Log("AddStudent", HttpContext.Session.GetString("UserName") ?? "unknown", s.MinistryCode);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var s = students.GetById(id); if (s is null) return NotFound();
        return View(new StudentEditVm { StudentId = id, Phone = s.Phone, Address = s.Address });
    }

    [HttpPost]
    public IActionResult Edit(StudentEditVm vm)
    {
        var s = students.GetById(vm.StudentId); if (s is null) return NotFound();
        s.Phone = vm.Phone; s.Address = vm.Address; students.Update(s);
        audit.Log("EditStudent", HttpContext.Session.GetString("UserName") ?? "unknown", s.MinistryCode);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Promote(int recordId, string academicYear, string grade, string section)
    {
        var record = records.GetById(recordId); if (record is null) return NotFound();
        promotion.Promote(record, academicYear, grade, section);
        return RedirectToAction(nameof(Details), new { id = record.StudentId });
    }
}
