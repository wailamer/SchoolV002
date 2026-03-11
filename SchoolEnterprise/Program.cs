using SchoolEnterprise.Helpers;
using SchoolEnterprise.Repositories;
using SchoolEnterprise.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<SeedData>();
    seed.EnsureSeeded();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapControllerRoute(name: "default", pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
