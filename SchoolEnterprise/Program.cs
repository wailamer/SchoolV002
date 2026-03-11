using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<XmlStorageService>();
builder.Services.AddSingleton<SequenceService>();
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

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PhotoUploadService>();
builder.Services.AddScoped<PromotionService>();
builder.Services.AddScoped<MarkCalculationService>();
builder.Services.AddScoped<AuditService>();
builder.Services.AddScoped<ClassOrderingService>();
builder.Services.AddScoped<ImportService>();
builder.Services.AddScoped<SeedData>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<SeedData>();
    seed.Initialize();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
