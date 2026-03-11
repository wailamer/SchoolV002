using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin")]
public class SettingsController(SchoolSettingsRepository repo) : Controller
{
    public IActionResult Index() => View(repo.Get() ?? new SchoolSettings());
    [HttpPost] public IActionResult Index(SchoolSettings model) { repo.Save(model); return RedirectToAction(nameof(Index)); }
}
