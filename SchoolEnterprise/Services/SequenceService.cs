namespace SchoolEnterprise.Services;

public class SequenceService
{
    private const string FileName = "sequences.xml";
    private readonly XmlStorageService _storage;

    public SequenceService(XmlStorageService storage) => _storage = storage;

    public int Next(string key)
    {
        var items = _storage.LoadList<SequenceItem>(FileName);
        var item = items.FirstOrDefault(x => x.Key == key);
        if (item is null)
        {
            item = new SequenceItem { Key = key, Value = 1 };
            items.Add(item);
        }
        else
        {
            item.Value++;
        }

        _storage.SaveList(FileName, items);
        return item.Value;
    }
}

public class SequenceItem
{
    public string Key { get; set; } = string.Empty;
    public int Value { get; set; }
}
