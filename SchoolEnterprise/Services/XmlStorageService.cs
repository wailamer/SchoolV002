using System.Xml.Serialization;

namespace SchoolEnterprise.Services;

public class XmlStorageService(IWebHostEnvironment env)
{
    private readonly string _dataDir = Path.Combine(env.ContentRootPath, "App_Data");

    public List<T> LoadList<T>(string fileName)
    {
        EnsureDir();
        var path = Path.Combine(_dataDir, fileName);
        if (!File.Exists(path))
        {
            SaveList(fileName, new List<T>());
            return [];
        }

        try
        {
            using var fs = File.OpenRead(path);
            var serializer = new XmlSerializer(typeof(List<T>));
            return serializer.Deserialize(fs) as List<T> ?? [];
        }
        catch
        {
            SaveList(fileName, new List<T>());
            return [];
        }
    }

    public void SaveList<T>(string fileName, List<T> data)
    {
        EnsureDir();
        var path = Path.Combine(_dataDir, fileName);
        using var fs = File.Create(path);
        var serializer = new XmlSerializer(typeof(List<T>));
        serializer.Serialize(fs, data);
    }

    private void EnsureDir()
    {
        if (!Directory.Exists(_dataDir))
            Directory.CreateDirectory(_dataDir);
    }
}
