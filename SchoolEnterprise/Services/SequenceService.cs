namespace SchoolEnterprise.Services;

public class SequenceService(XmlStorageService storage)
{
    private const string FileName = "sequences.xml";

    public int Next(string key)
    {
        var rows = storage.LoadList<SequenceRow>(FileName);
        var row = rows.FirstOrDefault(x => x.Key == key);
        if (row is null)
        {
            row = new SequenceRow { Key = key, Value = 1 };
            rows.Add(row);
        }
        else
        {
            row.Value++;
        }
        storage.SaveList(FileName, rows);
        return row.Value;
    }

    public class SequenceRow { public string Key { get; set; } = string.Empty; public int Value { get; set; } }
}
