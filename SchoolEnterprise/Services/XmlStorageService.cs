using System.Xml.Serialization;

namespace SchoolEnterprise.Services;

public class XmlStorageService(IWebHostEnvironment env)
{
    private readonly string _dataPath = Path.Combine(env.ContentRootPath, "App_Data");

    public List<T> LoadList<T>(string fileName)
    {
        Directory.CreateDirectory(_dataPath);
        var fullPath = Path.Combine(_dataPath, fileName);
        if (!File.Exists(fullPath))
        {
            SaveList(fileName, new List<T>());
            return new List<T>();
        }

        try
        {
            using var stream = File.OpenRead(fullPath);
            var serializer = new XmlSerializer(typeof(List<T>));
            return (List<T>?)serializer.Deserialize(stream) ?? [];
        }
        catch
        {
            SaveList(fileName, new List<T>());
            return [];
        }
    }

    public void SaveList<T>(string fileName, List<T> data)
    {
        Directory.CreateDirectory(_dataPath);
        var fullPath = Path.Combine(_dataPath, fileName);
        using var stream = File.Create(fullPath);
        var serializer = new XmlSerializer(typeof(List<T>));
        serializer.Serialize(stream, data);
    }
}
