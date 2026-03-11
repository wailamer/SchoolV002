using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class ClassOrderingService(StudentRecordRepository records, AuditService audit)
{
    public void Reorder(string grade, string section)
    {
        var list = records.GetByClass(grade, section).OrderBy(x => x.RollNumber).ToList();
        for (var i = 0; i < list.Count; i++)
        {
            list[i].RollNumber = i + 1;
            records.Update(list[i]);
        }
        audit.Log("ReorderClass", "system", $"{grade}-{section}");
    }
}
