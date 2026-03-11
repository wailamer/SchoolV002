using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class ClassOrderingService(StudentRecordRepository records, AuditService audit)
{
    public void Reorder(int classId, string username)
    {
        var rows = records.GetByClass(classId).OrderBy(x => x.RollNumber).ThenBy(x => x.StudentRecordId).ToList();
        for (var i = 0; i < rows.Count; i++) rows[i].RollNumber = i + 1;
        records.SaveAll(records.GetAll());
        audit.Log("ReorderClass", username, $"Class {classId}");
    }
}
