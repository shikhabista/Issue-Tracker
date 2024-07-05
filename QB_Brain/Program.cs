using System.Text;
using Base.Constants;
using Base.MigrationHistoryOverrides;
using Base.Providers;
using Base.Providers.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using IT_Web;
using IT_Web.Configuration;
using IT_Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
// services.AddSwaggerGen();
services.AddControllers(cnf => cnf.Filters.Add(new AuthorizeFilter()));
services.AddControllersWithViews().AddRazorRuntimeCompilation();
services.AddHttpContextAccessor();
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.ConfigureAppDi();
var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();
var configuration = serviceProvider.GetRequiredService<IConfiguration>();

services.AddDbContext<AppDbContext>(options => options
    .UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    // .ReplaceService<IHistoryRepository, MigrationHistory>());

services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/admin/login/index";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
    options.Cookie.Name = "auth";
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = "smart";
        opt.DefaultChallengeScheme = "smart";
    })
    .AddPolicyScheme("smart", "JWT or Identity Cookie", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader?.ToLower().StartsWith("bearer ") == true)
            {
                return JwtBearerDefaults.AuthenticationScheme;
            }

            return CookieAuthenticationDefaults.AuthenticationScheme;
        };
    })
    .AddCookie(cfg =>
    {
        cfg.SlidingExpiration = true;
        cfg.ExpireTimeSpan = TimeSpan.FromDays(2);
        cfg.LoginPath = "/admin/login/index";
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetJwtKey())),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });
services.AddAuthorization();


var context = serviceProvider.GetRequiredService<DbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
ConfigureStaticFiles(app, serviceProvider);

app.UseAuthentication();
app.UseAuthorization();
var authorizeAttribute = new AuthorizeAttribute() { AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme };

app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization(authorizeAttribute);
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}", defaults: new { area = "Admin" })
    .RequireAuthorization(authorizeAttribute);
app.Run();

void ConfigureStaticFiles(WebApplication webApplication, ServiceProvider serviceProvider1)
{
    var provider = new FileExtensionContentTypeProvider();
// Add new mappings
    provider.Mappings[".js"] = "application/javascript";
    webApplication.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
        ContentTypeProvider = provider
    });

    var contentPathProvider = serviceProvider1.GetRequiredService<IContentPathProvider>();
    var attachmentDir = contentPathProvider.GetPath(DirectoryType.Attachments).Result;
    var backupDir = contentPathProvider.GetPath(DirectoryType.Backup).Result;
    var dbRestoreDir = contentPathProvider.GetPath(DirectoryType.DbRestore).Result;
    webApplication.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(attachmentDir),
        RequestPath = DirRouteConstant.Attachments
    });

    
    webApplication.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(backupDir),
        RequestPath = DirRouteConstant.Backups
    });

    webApplication.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(dbRestoreDir),
        RequestPath = DirRouteConstant.DbRestore
    });
}