using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class PromotionService
{
    private readonly StudentRecordRepository _records;
    private readonly SequenceService _seq;

    public PromotionService(StudentRecordRepository records, SequenceService seq)
    {
        _records = records;
        _seq = seq;
    }

    public StudentRecord Promote(StudentRecord current, string nextYear, string nextGrade)
    {
        current.IsCurrent = false;
        _records.Update(current);

        var newRecord = new StudentRecord
        {
            StudentRecordId = _seq.Next("StudentRecordId"),
            StudentId = current.StudentId,
            AcademicYear = nextYear,
            Grade = nextGrade,
            Section = current.Section,
            ClassRoomId = current.ClassRoomId,
            RollNumber = current.RollNumber,
            IsCurrent = true
        };

        _records.Add(newRecord);
        return newRecord;
    }
}
