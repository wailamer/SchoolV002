using Microsoft.AspNetCore.Http;
using SchoolEnterprise.Models.Domain;

namespace SchoolEnterprise.Models.ViewModels;

public class LoginVm { public string Username { get; set; } = string.Empty; public string Password { get; set; } = string.Empty; }
public class StudentCreateVm
{
    public string MinistryCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string GrandFatherName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MotherName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; } = DateTime.Today;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
    public int ClassRoomId { get; set; }
    public int RollNumber { get; set; }
    public IFormFile? Photo { get; set; }
}
public class StudentEditVm : StudentCreateVm { public int StudentId { get; set; } }
public class AttendanceBoardVm { public int ClassRoomId { get; set; } public DateTime Date { get; set; } = DateTime.Today; public List<AttendanceStudentRowVm> Rows { get; set; } = []; }
public class AttendanceStudentRowVm { public int StudentRecordId { get; set; } public string StudentName { get; set; } = string.Empty; public bool IsPresent { get; set; } }
public class MarkEntryVm { public int ClassRoomId { get; set; } public int SubjectId { get; set; } public int Semester { get; set; } = 1; public List<StudentMark> Marks { get; set; } = []; }
public class TeacherCreateVm { public string FullName { get; set; } = string.Empty; public string Phone { get; set; } = string.Empty; public string Specialty { get; set; } = string.Empty; }
public class SubjectCreateVm { public string Name { get; set; } = string.Empty; public int TeacherId { get; set; } }
public class ClassCreateVm { public string Grade { get; set; } = string.Empty; public string Section { get; set; } = string.Empty; }
public class ImportCsvVm { public IFormFile? File { get; set; } }
public class ImportExcelVm { public IFormFile? File { get; set; } }
public class UserCreateVm { public string Username { get; set; } = string.Empty; public string Password { get; set; } = string.Empty; public string FullName { get; set; } = string.Empty; public UserRole Role { get; set; } }
public class StudentRecordEditVm { public int StudentRecordId { get; set; } public int ClassRoomId { get; set; } public int RollNumber { get; set; } public string Notes { get; set; } = string.Empty; }
public class TransferStudentVm { public int StudentRecordId { get; set; } public int ToClassRoomId { get; set; } public int NewRollNumber { get; set; } }
public class ClassReportRowVm { public string StudentName { get; set; } = string.Empty; public decimal Total { get; set; } public decimal Percentage { get; set; } public string Result { get; set; } = string.Empty; }
public class PrimaryReportVm { public StudentRegistration? Student { get; set; } public StudentRecord? Record { get; set; } public List<StudentMark> SemesterOne { get; set; } = []; public List<StudentMark> SemesterTwo { get; set; } = []; public decimal Percentage { get; set; } public string Result { get; set; } = string.Empty; public int AttendanceDays { get; set; } }
