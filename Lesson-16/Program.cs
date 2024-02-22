global using Microsoft.AspNetCore.Mvc;
using COMMON;
using Dapper;
using DBHelper;
using Hangfire;
using Hangfire.MemoryStorage;
using Lesson_16.DI_IOC;
using Lesson_16.Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using Serilog.Events;

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

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Error()
.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
.Enrich.FromLogContext()
.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null)
.CreateLogger();

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/kz/home/login/");
    options.AccessDeniedPath = new PathString("/kz/home/login/");
    options.LogoutPath = new PathString("/kz/home/signout/");
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

builder.Services.Configure<FormOptions>(o => {
    o.ValueLengthLimit = int.MaxValue;
    o.ValueCountLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
    o.KeyLengthLimit = int.MaxValue;
});
builder.Services.AddHangfire(x => x.UseMemoryStorage());
builder.Services.AddTransient<Lesson_16.Hangfire.SiteJob>();
builder.Services.AddHangfireServer();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

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
    name: "default",
    pattern: "{culture=kz}/{action=Index}/{query?}",
    defaults: new {controller="Home" ,action="Index"},
    constraints: new { culture = "kz|ru|en|zh-cn|tote|latyn|tr" }
);
app.MapControllerRoute(
    name: "home",
    pattern: "{culture=kz}/{controller=Home}/{action=Index}/{query?}",
    constraints: new { culture = "kz|ru|en|zh-cn|tote|latyn|tr" }
    );
    
app.MapFallbackToFile("404.html");
app.UseHangfireDashboard();
//BackgroundJob.Enqueue<SiteJob>((x) => x.JobRunAtMinutes()); //
//BackgroundJob.Schedule<SiteJob>((x) => x.JobRunAtMinutes(),TimeSpan.FromMinutes(1));
RecurringJob.AddOrUpdate<SiteJob>("jobRunAtMinutes",(x) => x.JobRunAtMinutes(),"*/1 * * * *");
RecurringJob.AddOrUpdate<SiteJob>("jobDeleteOldLogFiles",(x) => x.JobDeleteOldLogFiles(),Cron.Daily);
app.Run();