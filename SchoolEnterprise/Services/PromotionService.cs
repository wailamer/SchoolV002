using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class PromotionService(StudentRecordRepository records, SequenceService seq, AuditService audit)
{
    public StudentRecord Promote(StudentRecord current, int nextClassId, string year, string username)
    {
        current.Status = RecordStatus.Promoted;
        records.Update(current);
        var created = new StudentRecord
        {
            StudentRecordId = seq.Next("StudentRecordId"),
            StudentId = current.StudentId,
            ClassRoomId = nextClassId,
            AcademicYear = year,
            RollNumber = current.RollNumber,
            Status = RecordStatus.Active
        };
        records.Add(created);
        audit.Log("Promote", username, $"Record {current.StudentRecordId} => {created.StudentRecordId}");
        return created;
    }
}
