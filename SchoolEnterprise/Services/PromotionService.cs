using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class PromotionService(StudentRecordRepository records, SequenceService seq, AuditService audit)
{
    public StudentRecord Promote(StudentRecord current, string newYear, string newGrade, string newSection)
    {
        current.IsPromoted = true;
        records.Update(current);
        var next = new StudentRecord
        {
            StudentRecordId = seq.Next("StudentRecordId"),
            StudentId = current.StudentId,
            AcademicYear = newYear,
            Grade = newGrade,
            Section = newSection,
            RollNumber = current.RollNumber
        };
        records.Add(next);
        audit.Log("Promote", "system", $"Record {current.StudentRecordId} -> {next.StudentRecordId}");
        return next;
    }
}
