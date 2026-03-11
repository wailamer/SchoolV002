using Microsoft.AspNetCore.Mvc; using SchoolEnterprise.Helpers; using SchoolEnterprise.Models.ViewModels;
namespace SchoolEnterprise.Controllers; [RoleGuard("Admin","Secretary","Teacher")] public class ReportsController:Controller{ public IActionResult PrimaryReport()=>View(new PrimaryReportVm{SchoolName="مدرسة المؤسسة"}); public IActionResult MarkSheet()=>View(); }
