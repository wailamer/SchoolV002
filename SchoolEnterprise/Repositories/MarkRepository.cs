using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public class MarkRepository(XmlStorageService xml) : BaseXmlRepository<StudentMark>(xml, "marks.xml")
{
    public List<StudentMark> GetAll() => Read();
    public List<StudentMark> GetByRecordSemester(int recordId, int semester) => Read().Where(x => x.StudentRecordId == recordId && x.Semester == semester).ToList();
    public void Upsert(StudentMark entity)
    {
        var list = Read();
        var i = list.FindIndex(x => x.StudentRecordId == entity.StudentRecordId && x.SubjectId == entity.SubjectId && x.Semester == entity.Semester);
        if (i >= 0) list[i] = entity; else list.Add(entity);
        Write(list);
    }
}
