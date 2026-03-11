using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

[RoleGuard("Admin", "Secretary")]
public class ImportController(ImportService import) : Controller
{
    public IActionResult Index() => View();
    [HttpPost] public IActionResult Csv(ImportCsvVm vm) { if (vm.File is not null) { using var s = vm.File.OpenReadStream(); ViewBag.Count = import.ImportCsv(s);} return View("Index"); }
    [HttpPost] public IActionResult Excel(ImportExcelVm vm) { if (vm.File is not null) { using var s = vm.File.OpenReadStream(); ViewBag.Count = import.ImportExcel(s);} return View("Index"); }
    public IActionResult Sample() => File(System.Text.Encoding.UTF8.GetBytes("MinistryCode,FirstName,FatherName,GrandFatherName,LastName,MotherName,BirthDate,Phone,Address,AcademicYear,Grade,Section,RollNumber"), "text/csv", "students-sample.csv");
}
