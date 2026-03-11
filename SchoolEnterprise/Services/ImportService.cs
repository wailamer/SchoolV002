using OfficeOpenXml;
using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Models.ViewModels;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class ImportService(StudentRepository students, StudentRecordRepository records, SequenceService seq)
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
            var col = line.Split(',');
            if (col.Length < 13) continue;
            count += UpsertFromColumns(col);
        }
        return count;
    }

    public int ImportExcel(Stream stream)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage(stream);
        var ws = package.Workbook.Worksheets[0];
        var count = 0;
        for (int r = 2; r <= ws.Dimension.End.Row; r++)
        {
            var col = Enumerable.Range(1, 13).Select(c => ws.Cells[r, c].Text).ToArray();
            count += UpsertFromColumns(col);
        }
        return count;
    }

    private int UpsertFromColumns(string[] col)
    {
        if (students.GetByMinistryCode(col[0]) is not null) return 0;
        var student = new StudentRegistration
        {
            StudentId = seq.Next("StudentId"),
            GeneralRegisterNumber = $"GR-{seq.Next("GeneralRegister")}",
            MinistryCode = col[0],
            FirstName = col[1],
            FatherName = col[2],
            GrandFatherName = col[3],
            LastName = col[4],
            MotherName = col[5],
            BirthDate = DateTime.TryParse(col[6], out var d) ? d : DateTime.Today,
            Phone = col[7],
            Address = col[8]
        };
        students.Add(student);
        records.Add(new StudentRecord
        {
            StudentRecordId = seq.Next("StudentRecordId"),
            StudentId = student.StudentId,
            AcademicYear = col[9],
            ClassRoomId = 1,
            RollNumber = int.TryParse(col[12], out var rr) ? rr : 1
        });
        return 1;
    }
}
