using E_Commerce.DataAccess.Data; 
using Microsoft.EntityFrameworkCore;
using E_Commerce.DataAccess.Repository;
using E_Commerce.DataAccess.Repository.IRepository;
//using E_CommerceWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using E_Commerce.Utility;
using Stripe;
using E_Commerce.DataAccess.DbInitializer;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
    options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
});

builder.Services.AddAuthentication().AddGoogle(options =>
{
     options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
     options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
 });

builder.Services.AddAuthentication().AddMicrosoftAccount(options =>
  {
      options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
      options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
  });

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

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
StripeConfiguration.ApiKey = app.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
SeedDatabase();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase(){
    using (var scope = app.Services.CreateScope()){
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
