using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin","Secretary","Teacher")]
public class DashboardController(StudentRepository students, TeacherRepository teachers, ClassRepository classes) : Controller
{
    public IActionResult Index() { ViewBag.Students=students.GetAll().Count; ViewBag.Teachers=teachers.GetAll().Count; ViewBag.Classes=classes.GetAll().Count; return View(); }
}
