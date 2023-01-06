using Shqipify.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Shqipify.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<PostDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"))
);


builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ShqipifyDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

builder.Services.AddDbContext<ShqipifyDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}
app.Run();
