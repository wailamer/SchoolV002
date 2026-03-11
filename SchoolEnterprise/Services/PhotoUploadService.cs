namespace SchoolEnterprise.Services;

public class PhotoUploadService(IWebHostEnvironment env)
{
    public async Task<string> UploadStudentPhotoAsync(IFormFile? file)
    {
        if (file is null || file.Length == 0) return string.Empty;
        var folder = Path.Combine(env.WebRootPath, "uploads", "students");
        Directory.CreateDirectory(folder);
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(folder, fileName);
        await using var stream = File.Create(fullPath);
        await file.CopyToAsync(stream);
        return $"/uploads/students/{fileName}";
    }
}
