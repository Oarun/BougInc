using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using CulturNary.Web.Data;
using Microsoft.AspNetCore.Diagnostics;
using CulturNary.Web.Services;
using Microsoft.Extensions.Options;
using AspNetCore.ReCaptcha;
using Microsoft.Extensions.DependencyInjection;
using CulturNary.DAL.Abstract;
using CulturNary.DAL.Concrete;
using Microsoft.EntityFrameworkCore.Infrastructure;
using CulturNary.Web.Models;

var builder = WebApplication.CreateBuilder(args);
var appConnectionString = builder.Configuration.GetConnectionString("CulturNaryDbContextConnection") ?? throw new InvalidOperationException("Connection string 'CulturNaryDbContextConnection' not found.");
builder.Services.AddDbContext<CulturNaryDbContext>(options => options
    .UseLazyLoadingProxies()
    .UseSqlServer(appConnectionString));

builder.Services.AddScoped<DbContext,CulturNaryDbContext>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
//add a new repo builder.Services.AddScoped<interface, repo>();
// Add services to the container.
//change default connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
    .UseLazyLoadingProxies() 
    .UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//google sign-in
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });
builder.Services.AddReCaptcha(options => {
    options.SiteKey = builder.Configuration["Recaptcha:SiteKey"];
    options.SecretKey = builder.Configuration["Recaptcha:SecretKey"];
});

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerPathFeature = 
                    context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                var statusCode = context.Response.StatusCode;
                var errorRoute = $"/Home/Error/{statusCode}";

                context.Response.Redirect(errorRoute);
            });
        });
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");


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
