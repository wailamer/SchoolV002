using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin", "Secretary")]
public class SubjectsController(SubjectRepository repo, SequenceService seq) : Controller
{
    public IActionResult Index() => View(repo.GetAll());
    public IActionResult Create() => View(new SubjectCreateVm());
    [HttpPost] public IActionResult Create(SubjectCreateVm vm) { repo.Add(new Subject { SubjectId = seq.Next("SubjectId"), Name = vm.Name, Grade = vm.Grade, TeacherId = vm.TeacherId }); return RedirectToAction(nameof(Index)); }
    public IActionResult Edit(int id) { var s = repo.GetById(id); if (s is null) return NotFound(); return View(new SubjectCreateVm { Name = s.Name, Grade = s.Grade, TeacherId = s.TeacherId }); }
    [HttpPost] public IActionResult Edit(int id, SubjectCreateVm vm) { var s = repo.GetById(id); if (s is null) return NotFound(); s.Name = vm.Name; s.Grade = vm.Grade; s.TeacherId = vm.TeacherId; repo.Update(s); return RedirectToAction(nameof(Index)); }
    public IActionResult Delete(int id) { repo.Delete(id); return RedirectToAction(nameof(Index)); }
}
