global using Microsoft.AspNetCore.Mvc;
using COMMON;
using Dapper;
using DBHelper;
using Microsoft.AspNetCore.StaticFiles;


ElordaSingleton.GetInstance.SetConnectionString("server=localhost;port=3306;database=el_db;user=el_dba;password=12344321;charset=utf8mb4");
SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
SimpleCRUD.SetTableNameResolver(new ElordaResolver());


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{action=Register}/{id?}",
    defaults: new {controller="Home" ,action="Register"});

app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Register}/{id?}");
    
app.MapFallbackToFile("404.html");

app.Run();
