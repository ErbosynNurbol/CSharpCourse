global using Microsoft.AspNetCore.Mvc;
using System.Data;
using COMMON;
using Dapper;
using DBHelper;
using Lesson_16.DI_IOC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.StaticFiles;






var builder = WebApplication.CreateBuilder(args);

string defaultConnection = builder.Configuration["Ankui:DefaultConnection"].ToString();


builder.Services.AddTransient<ISiteInfo,SiteInfo>((iSiteInfo)=>{
    return new SiteInfo(){
         Connection = builder.Configuration["Ankui:DefaultConnection"].ToString(),
         SiteUrl = builder.Configuration["Ankui:SiteUrl"].ToString(),
         Port = int.TryParse(builder.Configuration["Ankui:Port"].ToString(),out int port)?port:0,
         SaveTime = DateTime.Now
    };
});




// builder.Services.AddSingleton(new SiteInfo(){
//     Connection = builder.Configuration["Ankui:DefaultConnection"].ToString(),
//     SiteUrl = builder.Configuration["Ankui:SiteUrl"].ToString(),
//     Port = int.TryParse(builder.Configuration["Ankui:Port"].ToString(),out int port)?port:0,
//     SaveTime = DateTime.Now
// });

ElordaSingleton.GetInstance.SetConnectionString(defaultConnection);


builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/home/login/");
    options.AccessDeniedPath = new PathString("/home/login/");
    options.LogoutPath = new PathString("/home/signout/");
    options.Cookie.Path = "/";
    options.SlidingExpiration = true;
    options.Cookie.Name = "qar_cookie";
    options.Cookie.HttpOnly = true; //JS 
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(Lesson_16.Filters.QarActionFilter));
});

builder.Services.AddSession();


// builder.Services.AddTransient<IMator,BMWMator>(); //Dependency Injection
// builder.Services.AddTransient<Car>(); //Inversation Of Control




var app = builder.Build();

SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
SimpleCRUD.SetTableNameResolver(new ElordaResolver());

// Car? myCar = app.Services.GetService<Car>();
// myCar?.Start();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings.Remove(".apk");
provider.Mappings.Add(".apk", "application/vnd.android.package-archive");
app.UseStaticFiles(new StaticFileOptions()
{
    ContentTypeProvider = provider,
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000");
    }
});

app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "admin",
    pattern: "{action=Index}/{query?}",
    defaults: new {controller="Home" ,action="Index"});

app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Index}/{query?}");
    
app.MapFallbackToFile("404.html");

app.Run();
