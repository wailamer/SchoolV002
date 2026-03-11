namespace SchoolEnterprise.Services;

public class PhotoUploadService
{
    private readonly IWebHostEnvironment _env;

    public PhotoUploadService(IWebHostEnvironment env) => _env = env;

    public async Task<string?> SaveStudentPhotoAsync(IFormFile? file)
    {
        if (file is null || file.Length == 0) return null;
        var dir = Path.Combine(_env.WebRootPath, "uploads", "students");
        Directory.CreateDirectory(dir);
        var name = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var path = Path.Combine(dir, name);
        using var stream = File.Create(path);
        await file.CopyToAsync(stream);
        return $"/uploads/students/{name}";
    }
}
