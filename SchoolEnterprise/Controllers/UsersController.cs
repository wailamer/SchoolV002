using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin")]
public class UsersController(UserRepository users, SequenceService seq) : Controller
{
    public IActionResult Index() => View(users.GetAll());
    public IActionResult Create() => View(new UserCreateVm());
    [HttpPost] public IActionResult Create(UserCreateVm vm) { users.Add(new AppUser { UserId = seq.Next("UserId"), UserName = vm.UserName, Password = vm.Password, Role = vm.Role }); return RedirectToAction(nameof(Index)); }
    public IActionResult Edit(int id) { var u = users.GetById(id); if (u is null) return NotFound(); return View(new UserCreateVm { UserName = u.UserName, Password = u.Password, Role = u.Role }); }
    [HttpPost] public IActionResult Edit(int id, UserCreateVm vm) { var u = users.GetById(id); if (u is null) return NotFound(); u.UserName = vm.UserName; u.Password = vm.Password; u.Role = vm.Role; users.Update(u); return RedirectToAction(nameof(Index)); }
    public IActionResult Delete(int id) { users.Delete(id); return RedirectToAction(nameof(Index)); }
}
