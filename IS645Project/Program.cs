using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IS645Project.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IS645ProjectContextConnection") ?? throw new InvalidOperationException("Connection string 'IS645ProjectContextConnection' not found.");

builder.Services.AddDbContext<IS645ProjectContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IS645ProjectUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IS645ProjectContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
