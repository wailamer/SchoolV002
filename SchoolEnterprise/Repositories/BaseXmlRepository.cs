using SchoolEnterprise.Services;

namespace SchoolEnterprise.Repositories;

public abstract class BaseXmlRepository<T>
{
    protected readonly XmlStorageService Storage;
    protected abstract string FileName { get; }

    protected BaseXmlRepository(XmlStorageService storage) => Storage = storage;

    public virtual List<T> GetAll() => Storage.LoadList<T>(FileName);
    protected void SaveAll(List<T> items) => Storage.SaveList(FileName, items);
}
