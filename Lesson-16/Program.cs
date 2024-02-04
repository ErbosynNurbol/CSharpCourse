global using Microsoft.AspNetCore.Mvc;
using COMMON;
using Dapper;
using DBHelper;
using Lesson_16.DI_IOC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.StaticFiles;

//DI  => Dependency Injection
//IOC => Inverion of Control



SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
SimpleCRUD.SetTableNameResolver(new ElordaResolver());


var builder = WebApplication.CreateBuilder(args);
string connectionStrings  = builder.Configuration["Elorda:DefaultConnection"].ToString();
ElordaSingleton.GetInstance.SetConnectionString(connectionStrings);


// builder.Services.AddTransient<Lesson_16.DI_IOC.ILogger,FileLogger>();
// builder.Services.AddTransient<Lesson_16.DI_IOC.App>();

// builder.Services.AddSingleton(new CurrencyInfo(){
//     USD2KZT =  450.1f,
//     EUR2KZT = 486.0f,
//     Time = DateTime.Now
// });




// builder.Services.AddTransient<CurrencyInfo>((s)=>{
//     return  new CurrencyInfo(){
//         USD2KZT =  450.1f,
//         EUR2KZT = 486.0f,
//         Time = DateTime.Now
//     };
// });

// builder.Services.AddScoped<CurrencyInfo>((s)=>{
//     return  new CurrencyInfo(){
//         USD2KZT =  450.1f,
//         EUR2KZT = 486.0f,
//         Time = DateTime.Now
//     };
// });


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

var app = builder.Build();

Lesson_16.DI_IOC.App? myApp = app.Services.GetService<Lesson_16.DI_IOC.App>();
myApp?.SaveLog("Progam cs save my log~");

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