using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin", "Secretary")]
public class RecordsController(StudentRecordRepository records, AuditService audit, ClassOrderingService ordering) : Controller
{
    public IActionResult Edit(int id) { var r = records.GetById(id); if (r is null) return NotFound(); return View(new StudentRecordEditVm { StudentRecordId = r.StudentRecordId, Grade = r.Grade, Section = r.Section, RollNumber = r.RollNumber }); }
    [HttpPost] public IActionResult Edit(StudentRecordEditVm vm) { var r = records.GetById(vm.StudentRecordId); if (r is null) return NotFound(); r.Grade = vm.Grade; r.Section = vm.Section; r.RollNumber = vm.RollNumber; records.Update(r); audit.Log("EditRecord", HttpContext.Session.GetString("UserName") ?? "unknown", vm.StudentRecordId.ToString()); return RedirectToAction("Details", "Students", new { id = r.StudentId }); }
    public IActionResult Transfer(int id) => View(new TransferStudentVm { StudentRecordId = id });
    [HttpPost] public IActionResult Transfer(TransferStudentVm vm) { var r = records.GetById(vm.StudentRecordId); if (r is null) return NotFound(); r.Section = vm.NewSection; r.RollNumber = vm.NewRollNumber; records.Update(r); audit.Log("TransferStudent", HttpContext.Session.GetString("UserName") ?? "unknown", vm.StudentRecordId.ToString()); return RedirectToAction("Details", "Students", new { id = r.StudentId }); }
    public IActionResult Archive() => View(records.GetAll().Where(x => x.IsArchived || x.IsPromoted).ToList());
    [HttpPost] public IActionResult ReorderClass(string grade, string section) { ordering.Reorder(grade, section); return RedirectToAction("Students", "Classes", new { grade, section }); }
}
