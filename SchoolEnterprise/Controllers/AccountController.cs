using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

public class AccountController(AuthService auth) : Controller
{
    [HttpGet] public IActionResult Login() => View(new LoginVm());
    [HttpPost] public IActionResult Login(LoginVm vm)
    {
        if (auth.Login(vm.Username, vm.Password)) return RedirectToAction("Index", "Dashboard");
        ViewBag.Error = "بيانات غير صحيحة";
        return View(vm);
    }
    public IActionResult Logout() { auth.Logout(); return RedirectToAction("Login"); }
}
