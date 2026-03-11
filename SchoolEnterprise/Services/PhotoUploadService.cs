namespace SchoolEnterprise.Services;

public class PhotoUploadService(IWebHostEnvironment env)
{
    public async Task<string> SaveStudentPhotoAsync(IFormFile? file)
    {
        if (file is null || file.Length == 0) return string.Empty;
        var uploads = Path.Combine(env.WebRootPath, "uploads", "students");
        Directory.CreateDirectory(uploads);
        var name = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var path = Path.Combine(uploads, name);
        using var fs = File.Create(path);
        await file.CopyToAsync(fs);
        return $"/uploads/students/{name}";
    }
}
