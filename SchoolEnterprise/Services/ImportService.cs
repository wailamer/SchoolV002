using OfficeOpenXml;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class ImportService
{
    private readonly StudentRepository _students;
    private readonly StudentRecordRepository _records;
    private readonly SequenceService _seq;

    public ImportService(StudentRepository students, StudentRecordRepository records, SequenceService seq)
    {
        _students = students;
        _records = records;
        _seq = seq;
    }

    public int ImportCsv(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var count = 0;
        _ = reader.ReadLine();
        while (!reader.EndOfStream)
        {
            var cols = (reader.ReadLine() ?? "").Split(',');
            if (cols.Length < 13 || _students.GetByMinistryCode(cols[0]) is not null) continue;
            AddStudent(cols);
            count++;
        }
        return count;
    }

    public int ImportExcel(Stream stream)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage(stream);
        var sheet = package.Workbook.Worksheets[0];
        var count = 0;
        for (var r = 2; r <= sheet.Dimension.End.Row; r++)
        {
            var cols = Enumerable.Range(1, 13).Select(c => sheet.Cells[r, c].Text).ToArray();
            if (string.IsNullOrWhiteSpace(cols[0]) || _students.GetByMinistryCode(cols[0]) is not null) continue;
            AddStudent(cols);
            count++;
        }
        return count;
    }

    private void AddStudent(string[] cols)
    {
        var student = new StudentRegistration
        {
            StudentId = _seq.Next("StudentId"),
            GeneralRegisterNumber = _seq.Next("GeneralRegister"),
            MinistryCode = cols[0], FirstName = cols[1], FatherName = cols[2], GrandFatherName = cols[3], LastName = cols[4],
            MotherName = cols[5], BirthDate = DateTime.TryParse(cols[6], out var d) ? d : DateTime.Today,
            Phone = cols[7], Address = cols[8]
        };
        _students.Add(student);
        _records.Add(new StudentRecord
        {
            StudentRecordId = _seq.Next("StudentRecordId"),
            StudentId = student.StudentId,
            AcademicYear = cols[9], Grade = cols[10], Section = cols[11], RollNumber = int.TryParse(cols[12], out var roll) ? roll : 1,
            IsCurrent = true
        });
    }
}
