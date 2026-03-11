using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin")]
public class AuditController(AuditLogRepository repo) : Controller
{
    public IActionResult Index() => View(repo.GetAll());
}
