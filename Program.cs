using System.Configuration;
using App.DBModels;
using App.Services;
using DAFW_IS220.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using App.Areas.Identity.Models.AccountViewModels;
using App.Menu;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using App.Data;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyShopContext>(options =>
{
    IConfiguration configuration = builder.Configuration;
    string connectString = configuration.GetConnectionString("MyShopContext") ?? "";
    options.UseMySql(connectString, ServerVersion.AutoDetect(connectString));

});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(cfg =>
{                    // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "appclothingshop";                 // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
});

builder.Services.AddTransient<CartService>();

builder.Services.AddRazorPages();

//Đăng ký dịch vụ identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<MyShopContext>()
                .AddDefaultTokenProviders();

// builder.Services.AddDefaultIdentity<AppUser>()
//                 .AddEntityFrameworkStores<MyShopContext>()
//                 .AddDefaultTokenProviders();

builder.Services.AddOptions();
var mailsetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsetting);
builder.Services.AddSingleton<IEmailSender, SendMailService>();

builder.Services.AddSingleton<RegisterViewModel, RegisterViewModel>();

builder.Services.AddScoped<RegisterViewModel>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lần thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất


    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = true;

});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login/";
    options.LogoutPath = "/logout/";
    options.AccessDeniedPath = "/khongduoctruycap.html";
});

builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddTransient<AdminSidebarService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewManageMenu", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireRole(RoleName.Administrator);
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewEditorMenu", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireRole(RoleName.Editor);
    });
});


builder.Services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        IConfiguration configuration = builder.Configuration;
                        var gconfig = configuration.GetSection("Authentication:Google");
                        options.ClientId = gconfig["ClientId"] ?? "";
                        options.ClientSecret = gconfig["ClientSecret"] ?? "";
                        // https://localhost:5001/signin-google
                        options.CallbackPath = "/dang-nhap-tu-google";
                    })
                    .AddFacebook(options =>
                    {
                        IConfiguration configuration = builder.Configuration;
                        var fconfig = configuration.GetSection("Authentication:Facebook");
                        options.AppId = fconfig["AppId"] ?? "";
                        options.AppSecret = fconfig["AppSecret"] ?? "";
                        options.CallbackPath = "/dang-nhap-tu-facebook";
                    })
                    // .AddTwitter()
                    // .AddMicrosoftAccount()
                    ;

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

// /contents/1.jpg => Uploads/1.jpg
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")
    ),
    RequestPath = "/contents"
});

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// [AcceptVerbs]
// [Route]
// [HttpGet]
// [HttpPost]
// [HttpPut]
// [HttpDelete]
// [HttpHead]
// [HttpPatch]

app.MapAreaControllerRoute(
    name: "admin",
    pattern: "{controller}/{action=Index}/{id?}",
    areaName: "Admin");

app.MapAreaControllerRoute(
    name: "identity",
    pattern: "{controller}/{action=Index}/{id?}",
    areaName: "Identity");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
