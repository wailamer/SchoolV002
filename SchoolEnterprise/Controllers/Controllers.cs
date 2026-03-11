using Microsoft.AspNetCore.Mvc;
using SchoolEnterprise.Helpers;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Controllers;

public class AccountController(AuthService auth) : Controller
{
    [HttpGet] public IActionResult Login() => View();
    [HttpPost] public IActionResult Login(LoginVm vm)
    {
        var user = auth.Login(vm.Username, vm.Password);
        if (user is null) { ViewBag.Error = "بيانات الدخول غير صحيحة"; return View(vm); }
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("Role", user.Role.ToString());
        return RedirectToAction("Index", "Dashboard");
    }
    public IActionResult Logout() { HttpContext.Session.Clear(); return RedirectToAction("Login"); }
}

[RoleGuard]
public class DashboardController(StudentRepository students, StudentRecordRepository records, TeacherRepository teachers) : Controller
{
    public IActionResult Index() { ViewBag.Students = students.GetAll().Count; ViewBag.Records = records.GetAll().Count; ViewBag.Teachers = teachers.GetAll().Count; return View(); }
}

[RoleGuard(UserRole.Admin, UserRole.Secretary)]
public class StudentsController(StudentRepository students, StudentRecordRepository records, ClassRepository classes, SequenceService seq, PhotoUploadService photo, AuditService audit, PromotionService promo) : Controller
{
    public IActionResult Index() => View(students.GetAll());
    public IActionResult Details(int id) => View(students.GetById(id));
    public IActionResult Create() { ViewBag.Classes = classes.GetAll(); return View(); }
    [HttpPost] public async Task<IActionResult> Create(StudentCreateVm vm)
    {
        if (students.GetByMinistryCode(vm.MinistryCode) is not null) { ModelState.AddModelError("MinistryCode", "الكود الوزاري مستخدم"); ViewBag.Classes = classes.GetAll(); return View(vm); }
        var row = new StudentRegistration { StudentId = seq.Next("StudentId"), GeneralRegisterNumber = $"GR-{seq.Next("GeneralRegister")}", MinistryCode = vm.MinistryCode, FirstName = vm.FirstName, FatherName = vm.FatherName, GrandFatherName = vm.GrandFatherName, LastName = vm.LastName, MotherName = vm.MotherName, BirthDate = vm.BirthDate, Phone = vm.Phone, Address = vm.Address, PhotoPath = await photo.SaveStudentPhotoAsync(vm.Photo) };
        students.Add(row);
        records.Add(new StudentRecord { StudentRecordId = seq.Next("StudentRecordId"), StudentId = row.StudentId, AcademicYear = vm.AcademicYear, ClassRoomId = vm.ClassRoomId, RollNumber = vm.RollNumber });
        audit.Log("AddStudent", HttpContext.Session.GetString("Username") ?? "system", row.MinistryCode);
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Edit(int id) { var s = students.GetById(id); if (s is null) return NotFound(); ViewBag.Classes = classes.GetAll(); return View(new StudentEditVm { StudentId = s.StudentId, MinistryCode = s.MinistryCode, FirstName = s.FirstName, FatherName = s.FatherName, GrandFatherName = s.GrandFatherName, LastName = s.LastName, MotherName = s.MotherName, BirthDate = s.BirthDate, Phone = s.Phone, Address = s.Address, AcademicYear = records.GetCurrentByStudentId(id)?.AcademicYear ?? "", ClassRoomId = records.GetCurrentByStudentId(id)?.ClassRoomId ?? 0, RollNumber = records.GetCurrentByStudentId(id)?.RollNumber ?? 1 }); }
    [HttpPost] public IActionResult Edit(StudentEditVm vm) { var s = students.GetById(vm.StudentId); if (s is null) return NotFound(); s.FirstName = vm.FirstName; s.FatherName = vm.FatherName; s.LastName = vm.LastName; s.Phone = vm.Phone; s.Address = vm.Address; students.Update(s); var r = records.GetCurrentByStudentId(s.StudentId); if (r is not null) { r.ClassRoomId = vm.ClassRoomId; r.RollNumber = vm.RollNumber; r.AcademicYear = vm.AcademicYear; records.Update(r); } audit.Log("EditStudent", HttpContext.Session.GetString("Username") ?? "system", s.StudentId.ToString()); return RedirectToAction(nameof(Index)); }
    public IActionResult Promote(int id, int nextClassId, string year) { var rec = records.GetCurrentByStudentId(id); if (rec is null) return NotFound(); promo.Promote(rec, nextClassId, year, HttpContext.Session.GetString("Username") ?? "system"); return RedirectToAction(nameof(Details), new { id }); }
}

[RoleGuard(UserRole.Admin, UserRole.Secretary)]
public class TeachersController(TeacherRepository repo, SequenceService seq) : Controller { public IActionResult Index()=>View(repo.GetAll()); public IActionResult Create()=>View(); [HttpPost] public IActionResult Create(TeacherCreateVm vm){repo.Add(new Teacher{TeacherId=seq.Next("TeacherId"),FullName=vm.FullName,Phone=vm.Phone,Specialty=vm.Specialty});return RedirectToAction(nameof(Index));} public IActionResult Edit(int id)=>View(repo.GetById(id)); [HttpPost] public IActionResult Edit(Teacher t){repo.Update(t);return RedirectToAction(nameof(Index));} public IActionResult Delete(int id){repo.Delete(id);return RedirectToAction(nameof(Index));}}
[RoleGuard(UserRole.Admin, UserRole.Secretary)]
public class SubjectsController(SubjectRepository repo, TeacherRepository teachers, SequenceService seq) : Controller { public IActionResult Index()=>View(repo.GetAll()); public IActionResult Create(){ViewBag.Teachers=teachers.GetAll(); return View();} [HttpPost] public IActionResult Create(SubjectCreateVm vm){repo.Add(new Subject{SubjectId=seq.Next("SubjectId"),Name=vm.Name,TeacherId=vm.TeacherId});return RedirectToAction(nameof(Index));} public IActionResult Edit(int id){ViewBag.Teachers=teachers.GetAll(); return View(repo.GetById(id));} [HttpPost] public IActionResult Edit(Subject m){repo.Update(m);return RedirectToAction(nameof(Index));} public IActionResult Delete(int id){repo.Delete(id);return RedirectToAction(nameof(Index));}}
[RoleGuard(UserRole.Admin, UserRole.Secretary)]
public class ClassesController(ClassRepository repo, StudentRecordRepository records, SequenceService seq) : Controller { public IActionResult Index()=>View(repo.GetAll()); public IActionResult Create()=>View(); [HttpPost] public IActionResult Create(ClassCreateVm vm){repo.Add(new ClassRoom{ClassRoomId=seq.Next("ClassId"),Grade=vm.Grade,Section=vm.Section});return RedirectToAction(nameof(Index));} public IActionResult Edit(int id)=>View(repo.GetById(id)); [HttpPost] public IActionResult Edit(ClassRoom m){repo.Update(m);return RedirectToAction(nameof(Index));} public IActionResult Delete(int id){repo.Delete(id);return RedirectToAction(nameof(Index));} public IActionResult Students(int id)=>View(records.GetByClass(id)); }

[RoleGuard(UserRole.Admin, UserRole.Secretary, UserRole.Teacher)]
public class AttendanceController(StudentRecordRepository records, StudentRepository students, AttendanceRepository attendance, SequenceService seq) : Controller
{
    public IActionResult Board(int classId = 1)
    {
        var vm = new AttendanceBoardVm { ClassRoomId = classId, Rows = records.GetByClass(classId).Select(r => new AttendanceStudentRowVm { StudentRecordId = r.StudentRecordId, StudentName = students.GetById(r.StudentId)?.FirstName + " " + students.GetById(r.StudentId)?.LastName }).ToList() };
        return View(vm);
    }
    [HttpPost] public IActionResult Save(AttendanceBoardVm vm) { foreach (var row in vm.Rows) attendance.Upsert(new AttendanceEntry { AttendanceId = seq.Next("AttendanceId"), StudentRecordId = row.StudentRecordId, Date = vm.Date, IsPresent = row.IsPresent }); return RedirectToAction(nameof(Board), new { classId = vm.ClassRoomId }); }
}

[RoleGuard(UserRole.Admin, UserRole.Secretary, UserRole.Teacher)]
public class MarksController(StudentRecordRepository records, SubjectRepository subjects, MarkRepository marks, SequenceService seq, MarkCalculationService calc) : Controller
{
    public IActionResult Enter(int classId = 1, int subjectId = 1, int semester = 1)
    {
        var vm = new MarkEntryVm { ClassRoomId = classId, SubjectId = subjectId, Semester = semester, Marks = records.GetByClass(classId).Select(r => marks.GetByRecordSemester(r.StudentRecordId, semester).FirstOrDefault(x => x.SubjectId == subjectId) ?? new StudentMark { MarkId = seq.Next("MarkId"), StudentRecordId = r.StudentRecordId, SubjectId = subjectId, Semester = semester }).ToList() };
        ViewBag.Subjects = subjects.GetAll();
        return View(vm);
    }
    [HttpPost] public IActionResult Save(MarkEntryVm vm) { foreach (var m in vm.Marks) { var c = calc.Calculate(m.Activities, m.Oral, m.Homework, m.Exam); m.Total = c.total; m.GradeLetter = c.grade; marks.Upsert(m); } return RedirectToAction(nameof(Enter), new { classId = vm.ClassRoomId, subjectId = vm.SubjectId, semester = vm.Semester }); }
}

[RoleGuard(UserRole.Admin, UserRole.Secretary, UserRole.Teacher)]
public class ReportsController(StudentRepository students, StudentRecordRepository records, MarkRepository marks, AttendanceRepository attendance, MarkCalculationService calc, SchoolSettingsRepository settings) : Controller
{
    public IActionResult PrimaryReport(int studentId)
    {
        var s = students.GetById(studentId); if (s is null) return NotFound(); var r = records.GetCurrentByStudentId(studentId); if (r is null) return NotFound();
        var sem1 = marks.GetByRecordSemester(r.StudentRecordId, 1); var sem2 = marks.GetByRecordSemester(r.StudentRecordId, 2);
        var pct = calc.Percentage(sem1.Select(x => x.Total).Concat(sem2.Select(x => x.Total)));
        return View(new PrimaryReportVm { Student = s, Record = r, SemesterOne = sem1, SemesterTwo = sem2, Percentage = pct, Result = pct >= 50 ? "ناجح" : "راسب", AttendanceDays = attendance.GetByRecord(r.StudentRecordId).Count(x => x.IsPresent) });
    }
    public IActionResult MarkSheet(int studentId) => PrimaryReport(studentId);
}

[RoleGuard(UserRole.Admin, UserRole.Secretary)]
public class ImportController(ImportService import, AuditService audit) : Controller
{
    public IActionResult Index() => View();
    [HttpPost] public IActionResult Csv(ImportCsvVm vm) { if (vm.File is null) return RedirectToAction(nameof(Index)); var c = import.ImportCsv(vm.File.OpenReadStream()); audit.Log("ImportCsv", HttpContext.Session.GetString("Username") ?? "system", c.ToString()); TempData["msg"] = $"Imported {c}"; return RedirectToAction(nameof(Index)); }
    [HttpPost] public IActionResult Excel(ImportExcelVm vm) { if (vm.File is null) return RedirectToAction(nameof(Index)); var c = import.ImportExcel(vm.File.OpenReadStream()); audit.Log("ImportExcel", HttpContext.Session.GetString("Username") ?? "system", c.ToString()); TempData["msg"] = $"Imported {c}"; return RedirectToAction(nameof(Index)); }
    public IActionResult Sample() { var bytes = System.Text.Encoding.UTF8.GetBytes("MinistryCode,FirstName,FatherName,GrandFatherName,LastName,MotherName,BirthDate,Phone,Address,AcademicYear,Grade,Section,RollNumber\nM-2001,سالم,نزار,عدنان,الحسن,ليلى,2015-03-01,09111,دمشق,2025-2026,الخامس,A,2"); return File(bytes, "text/csv", "sample-students.csv"); }
}

[RoleGuard(UserRole.Admin)]
public class UsersController(UserRepository repo, SequenceService seq) : Controller { public IActionResult Index()=>View(repo.GetAll()); public IActionResult Create()=>View(); [HttpPost] public IActionResult Create(UserCreateVm vm){repo.Add(new AppUser{UserId=seq.Next("UserId"),Username=vm.Username,Password=vm.Password,FullName=vm.FullName,Role=vm.Role}); return RedirectToAction(nameof(Index));} public IActionResult Edit(int id)=>View(repo.GetById(id)); [HttpPost] public IActionResult Edit(AppUser m){repo.Update(m); return RedirectToAction(nameof(Index));} public IActionResult Delete(int id){repo.Delete(id); return RedirectToAction(nameof(Index));}}

[RoleGuard(UserRole.Admin, UserRole.Secretary)]
public class RecordsController(StudentRecordRepository repo, ClassOrderingService ordering, AuditService audit) : Controller
{
    public IActionResult Edit(int id) => View(repo.GetById(id));
    [HttpPost] public IActionResult Edit(StudentRecordEditVm vm) { var r = repo.GetById(vm.StudentRecordId); if (r is null) return NotFound(); r.ClassRoomId = vm.ClassRoomId; r.RollNumber = vm.RollNumber; r.Notes = vm.Notes; repo.Update(r); audit.Log("EditRecord", HttpContext.Session.GetString("Username") ?? "system", r.StudentRecordId.ToString()); return RedirectToAction("Index", "Students"); }
    public IActionResult Transfer(int id) => View(new TransferStudentVm { StudentRecordId = id });
    [HttpPost] public IActionResult Transfer(TransferStudentVm vm) { var r = repo.GetById(vm.StudentRecordId); if (r is null) return NotFound(); r.ClassRoomId = vm.ToClassRoomId; r.RollNumber = vm.NewRollNumber; repo.Update(r); audit.Log("Transfer", HttpContext.Session.GetString("Username") ?? "system", r.StudentRecordId.ToString()); return RedirectToAction("Index", "Students"); }
    public IActionResult Archive() => View(repo.GetAll().Where(x => x.Status != RecordStatus.Active).ToList());
    public IActionResult ReorderClass(int classId) { ordering.Reorder(classId, HttpContext.Session.GetString("Username") ?? "system"); return RedirectToAction("Students", "Classes", new { id = classId }); }
}

[RoleGuard(UserRole.Admin, UserRole.Secretary, UserRole.Teacher)]
public class ClassReportsController(StudentRecordRepository records, StudentRepository students, MarkRepository marks, MarkCalculationService calc) : Controller
{
    public IActionResult Index(int classId = 1)
    {
        var list = records.GetByClass(classId).Select(r => {
            var st = students.GetById(r.StudentId); var totals = marks.GetByRecordSemester(r.StudentRecordId, 1).Concat(marks.GetByRecordSemester(r.StudentRecordId,2)).Select(x=>x.Total).ToList();
            var pct = calc.Percentage(totals);
            return new ClassReportRowVm { StudentName = $"{st?.FirstName} {st?.LastName}", Total = totals.Sum(), Percentage = pct, Result = pct >= 50 ? "ناجح" : "راسب" };
        }).ToList();
        return View(list);
    }
}

[RoleGuard(UserRole.Admin)]
public class SettingsController(SchoolSettingsRepository repo) : Controller { public IActionResult Index()=>View(repo.Get() ?? new SchoolSettings()); [HttpPost] public IActionResult Index(SchoolSettings s){repo.Save(s);return RedirectToAction(nameof(Index));} }
[RoleGuard(UserRole.Admin)]
public class AuditController(AuditLogRepository repo) : Controller { public IActionResult Index()=>View(repo.GetAll().OrderByDescending(x=>x.Timestamp).ToList()); }
