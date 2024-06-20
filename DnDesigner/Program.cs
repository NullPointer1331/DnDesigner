using DnDesigner.Data;
using DnDesigner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DnDesignerDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<IDBHelper, DBHelper>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DnDesignerDbContext>();
builder.Services.AddControllersWithViews();

Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<DnDesignerDbContext>(options =>
                    options.UseSqlServer(connectionString));
                services.AddDatabaseDeveloperPageExceptionFilter();

                services.AddTransient<IDBHelper, DBHelper>();

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<DnDesignerDbContext>();
                services.AddControllersWithViews();

                /*services.Configure<ElasticEmailOptions>(options =>
                {
                    options.ApiKey = "";
                });*/

                /*services.AddTransient<IEmailSender, EmailSender>();*//*
                services.AddScoped<EmailSender>();*/
            });

var app = builder.Build();

using IServiceScope scope = app.Services.CreateScope();
#if DEBUG
// Apply migrations on startup in debug mode
var db = scope.ServiceProvider.GetRequiredService<DnDesignerDbContext>();
db.Database.Migrate();
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();