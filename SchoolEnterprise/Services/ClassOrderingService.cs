using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class ClassOrderingService
{
    private readonly StudentRecordRepository _records;

    public ClassOrderingService(StudentRecordRepository records) => _records = records;

    public void Reorder(int classRoomId)
    {
        var list = _records.GetByClass(classRoomId).OrderBy(r => r.RollNumber).ToList();
        for (var i = 0; i < list.Count; i++)
        {
            list[i].RollNumber = i + 1;
            _records.Update(list[i]);
        }
    }
}
