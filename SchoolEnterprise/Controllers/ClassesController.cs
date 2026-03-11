using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin", "Secretary")]
public class ClassesController(ClassRepository repo, StudentRecordRepository records, SequenceService seq) : Controller
{
    public IActionResult Index() => View(repo.GetAll());
    public IActionResult Students(string grade, string section) => View(records.GetByClass(grade, section));
    public IActionResult Create() => View(new ClassCreateVm());
    [HttpPost] public IActionResult Create(ClassCreateVm vm) { repo.Add(new ClassRoom { ClassRoomId = seq.Next("ClassRoomId"), Grade = vm.Grade, Section = vm.Section, AcademicYear = vm.AcademicYear }); return RedirectToAction(nameof(Index)); }
    public IActionResult Edit(int id) { var c = repo.GetById(id); if (c is null) return NotFound(); return View(new ClassCreateVm { Grade = c.Grade, Section = c.Section, AcademicYear = c.AcademicYear }); }
    [HttpPost] public IActionResult Edit(int id, ClassCreateVm vm) { var c = repo.GetById(id); if (c is null) return NotFound(); c.Grade = vm.Grade; c.Section = vm.Section; c.AcademicYear = vm.AcademicYear; repo.Update(c); return RedirectToAction(nameof(Index)); }
    public IActionResult Delete(int id) { repo.Delete(id); return RedirectToAction(nameof(Index)); }
}
