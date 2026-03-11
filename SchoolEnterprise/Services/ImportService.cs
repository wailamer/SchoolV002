using OfficeOpenXml;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class ImportService(StudentRepository students, StudentRecordRepository records, SequenceService seq, AuditService audit)
{
    public int ImportCsv(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var count = 0;
        _ = reader.ReadLine();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            var c = line.Split(',');
            if (c.Length < 13 || students.GetByMinistryCode(c[0]) is not null) continue;
            var student = CreateStudent(c);
            students.Add(student);
            records.Add(CreateRecord(student.StudentId, c));
            count++;
        }
        audit.Log("ImportCsv", "system", $"{count} students imported");
        return count;
    }

    public int ImportExcel(Stream stream)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage(stream);
        var ws = package.Workbook.Worksheets[0];
        var row = 2;
        var count = 0;
        while (ws.Cells[row, 1].Value is not null)
        {
            var c = Enumerable.Range(1, 13).Select(i => ws.Cells[row, i].Text).ToArray();
            if (students.GetByMinistryCode(c[0]) is null)
            {
                var student = CreateStudent(c);
                students.Add(student);
                records.Add(CreateRecord(student.StudentId, c));
                count++;
            }
            row++;
        }
        audit.Log("ImportExcel", "system", $"{count} students imported");
        return count;
    }

    private StudentRegistration CreateStudent(string[] c) => new()
    {
        StudentId = seq.Next("StudentId"),
        GeneralRegisterNumber = seq.Next("GeneralRegister"),
        MinistryCode = c[0],
        FirstName = c[1],
        FatherName = c[2],
        GrandFatherName = c[3],
        LastName = c[4],
        MotherName = c[5],
        BirthDate = DateTime.TryParse(c[6], out var d) ? d : DateTime.Today,
        Phone = c[7],
        Address = c[8]
    };

    private StudentRecord CreateRecord(int studentId, string[] c) => new()
    {
        StudentRecordId = seq.Next("StudentRecordId"),
        StudentId = studentId,
        AcademicYear = c[9],
        Grade = c[10],
        Section = c[11],
        RollNumber = int.TryParse(c[12], out var n) ? n : 1
    };
}
