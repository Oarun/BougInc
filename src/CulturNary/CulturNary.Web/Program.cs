using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using CulturNary.Web.Data;
using Microsoft.AspNetCore.Diagnostics;
using CulturNary.Web.Services;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.Extensions.Options;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using CulturNary.DAL.Abstract;
using CulturNary.DAL.Concrete;
using Microsoft.EntityFrameworkCore.Infrastructure;
using CulturNary.Web.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

var builder = WebApplication.CreateBuilder(args);
var appConnectionString = builder.Configuration.GetConnectionString("CulturNaryDbContextConnection") ?? throw new InvalidOperationException("Connection string 'CulturNaryDbContextConnection' not found.");
builder.Services.AddDbContext<CulturNaryDbContext>(options => options
    .UseLazyLoadingProxies()
    .UseSqlServer(appConnectionString));


builder.Services.AddScoped<DbContext,CulturNaryDbContext>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IRecipeSearchService, RecipeSearchService>();
builder.Services.AddScoped<IFavoriteRecipeRepository, FavoriteRecipeRepository>();

//add a new repo builder.Services.AddScoped<interface, repo>();
// Add services to the container.
//change default connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
    .UseLazyLoadingProxies() 
    .UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<SiteUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", 
                        policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequiresSignedRole",
                        policy => policy.RequireRole("Signed"));
});

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.Configure<AzureStorageConfig>(builder.Configuration.GetSection("AzureStorageConfig"));

builder.Services.AddScoped<ImageStorageService>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!roleManager.RoleExistsAsync("Admin").Result)
    {
        roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
    }

    if (!roleManager.RoleExistsAsync("Signed").Result)
    {
        roleManager.CreateAsync(new IdentityRole("Signed")).Wait();
    }
}
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
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
