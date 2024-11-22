using IS645Project.Middleware;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IS645ProjectContextConnection") ?? throw new InvalidOperationException("Connection string 'IS645ProjectContextConnection' not found.");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Logs to the console
builder.Logging.AddDebug();   // Logs to the Debug output window
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseMiddleware<RedirectToLoginMiddleware>();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
