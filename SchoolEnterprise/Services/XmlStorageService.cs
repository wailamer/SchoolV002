using System.Xml.Serialization;

namespace SchoolEnterprise.Services;

public class XmlStorageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly object _lock = new();

    public XmlStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    private string ResolvePath(string fileName)
    {
        var dir = Path.Combine(_environment.ContentRootPath, "App_Data");
        Directory.CreateDirectory(dir);
        return Path.Combine(dir, fileName);
    }

    public List<T> LoadList<T>(string fileName)
    {
        var path = ResolvePath(fileName);
        lock (_lock)
        {
            if (!File.Exists(path))
            {
                SaveList(fileName, new List<T>());
                return new List<T>();
            }

            try
            {
                using var stream = File.OpenRead(path);
                var serializer = new XmlSerializer(typeof(List<T>));
                return serializer.Deserialize(stream) as List<T> ?? new List<T>();
            }
            catch
            {
                SaveList(fileName, new List<T>());
                return new List<T>();
            }
        }
    }

    public void SaveList<T>(string fileName, List<T> items)
    {
        var path = ResolvePath(fileName);
        lock (_lock)
        {
            using var stream = File.Create(path);
            var serializer = new XmlSerializer(typeof(List<T>));
            serializer.Serialize(stream, items);
        }
    }
}
