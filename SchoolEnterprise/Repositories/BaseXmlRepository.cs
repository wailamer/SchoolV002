using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public abstract class BaseXmlRepository<T>(XmlStorageService xml, string fileName)
{
    protected List<T> Read() => xml.LoadList<T>(fileName);
    protected void Write(List<T> items) => xml.SaveList(fileName, items);
}
