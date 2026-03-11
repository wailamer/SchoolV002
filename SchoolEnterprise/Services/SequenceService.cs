using System.Xml.Serialization;

namespace SchoolEnterprise.Services;

public class SequenceService(XmlStorageService xml)
{
    private const string FileName = "sequences.xml";

    public int Next(string key)
    {
        var list = xml.LoadList<SequenceItem>(FileName);
        var item = list.FirstOrDefault(x => x.Key == key);
        if (item is null)
        {
            item = new SequenceItem { Key = key, Value = 1 };
            list.Add(item);
        }
        else
        {
            item.Value++;
        }

        xml.SaveList(FileName, list);
        return item.Value;
    }

    public class SequenceItem
    {
        public string Key { get; set; } = string.Empty;
        public int Value { get; set; }
    }
}
