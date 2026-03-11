using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

public class AccountController(AuthService auth) : Controller
{
    public IActionResult Login() => View(new LoginVm());

    [HttpPost]
    public IActionResult Login(LoginVm vm)
    {
        var user = auth.Login(vm.UserName, vm.Password);
        if (user is null)
        {
            ViewBag.Error = "بيانات الدخول غير صحيحة";
            return View(vm);
        }

        HttpContext.Session.SetString("UserName", user.UserName);
        HttpContext.Session.SetString("Role", user.Role);
        return RedirectToAction("Index", "Dashboard");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}
