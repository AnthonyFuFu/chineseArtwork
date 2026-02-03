using chineseArtwork.Dao.Implementations;
using chineseArtwork.Dao.Interfaces;
using chineseArtwork.Models;
using chineseArtwork.Services.Implementations;
using chineseArtwork.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ChineseArtworkContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChineseArtworkConnection")));

builder.Services.AddScoped<IMemberDao, MemberDao>();

builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapRazorPages();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
