using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddSingleton<XmlStorageService>();
builder.Services.AddSingleton<SequenceService>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<PhotoUploadService>();
builder.Services.AddSingleton<PromotionService>();
builder.Services.AddSingleton<MarkCalculationService>();
builder.Services.AddSingleton<AuditService>();
builder.Services.AddSingleton<ClassOrderingService>();
builder.Services.AddSingleton<ImportService>();
builder.Services.AddSingleton<SeedData>();

builder.Services.AddSingleton<StudentRepository>();
builder.Services.AddSingleton<StudentRecordRepository>();
builder.Services.AddSingleton<TeacherRepository>();
builder.Services.AddSingleton<SubjectRepository>();
builder.Services.AddSingleton<ClassRepository>();
builder.Services.AddSingleton<AttendanceRepository>();
builder.Services.AddSingleton<MarkRepository>();
builder.Services.AddSingleton<SchoolSettingsRepository>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<AuditLogRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<SeedData>();
    seeder.EnsureSeeded();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
