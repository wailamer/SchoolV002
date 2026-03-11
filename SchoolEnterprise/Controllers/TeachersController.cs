using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin", "Secretary")]
public class TeachersController(TeacherRepository repo, SequenceService seq) : Controller
{
    public IActionResult Index() => View(repo.GetAll());
    public IActionResult Create() => View(new TeacherCreateVm());
    [HttpPost] public IActionResult Create(TeacherCreateVm vm) { repo.Add(new Teacher { TeacherId = seq.Next("TeacherId"), FullName = vm.FullName, Phone = vm.Phone, Specialization = vm.Specialization }); return RedirectToAction(nameof(Index)); }
    public IActionResult Edit(int id) { var t = repo.GetById(id); if (t is null) return NotFound(); return View(new TeacherCreateVm { FullName = t.FullName, Phone = t.Phone, Specialization = t.Specialization }); }
    [HttpPost] public IActionResult Edit(int id, TeacherCreateVm vm) { var t = repo.GetById(id); if (t is null) return NotFound(); t.FullName = vm.FullName; t.Phone = vm.Phone; t.Specialization = vm.Specialization; repo.Update(t); return RedirectToAction(nameof(Index)); }
    public IActionResult Delete(int id) { repo.Delete(id); return RedirectToAction(nameof(Index)); }
}
