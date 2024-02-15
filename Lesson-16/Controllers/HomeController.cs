using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Lesson_16.Helpers;
using Microsoft.AspNetCore.Http;
using COMMON;
using MODEL;
using DBHelper;
using Dapper;
using MODEL.FormatModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ZstdSharp.Unsafe;
using Lesson_16.DI_IOC;
using Microsoft.Extensions.Caching.Memory;
using Google.Protobuf.WellKnownTypes;
using Lesson_16.Cache;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
//yafo jszp qewe vjry
namespace Lesson_16.Controllers;

[Authorize]
public class HomeController : QarBaseController
{
    IConfiguration _configuration;
    IWebHostEnvironment _webHostEnvironment;
    IMemoryCache _memoryCache;

    public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment,IMemoryCache memoryCache)
    {
         _configuration = configuration;
          _webHostEnvironment = webHostEnvironment;
          _memoryCache = memoryCache;
    }

     [AllowAnonymous]
     public IActionResult Article(string query)
     {
         query = (query??string.Empty).ToLower();
         query =string.IsNullOrEmpty(query)?"list":query;
         if(query.Equals("list"))
         {  
                int page = GetIntQueryParam("page",1);
                page = page<=0?1:page;
                int pageSize = GetIntQueryParam("pageSize",10);
                pageSize = pageSize<=0?10:pageSize;
                string keyWord = GetStringQueryParam("keyWord");
                ViewData["page"] = page;
                ViewData["pageSize"] = pageSize;
                ViewData["keyWord"] = keyWord;
                using(var _connection = Utilities.GetOpenConnection())
                {
                   string querySql = "where qStatus = 0 ";
                   if(!string.IsNullOrEmpty(keyWord))
                   {
                        querySql += " and MATCH(title,fullDescription) AGAINST(@keyWord IN NATURAL LANGUAGE MODE) ";
                   }
                   object queryObj = new {keyWord,start = (page-1)*pageSize,length = pageSize};
                   int totalCount = _connection.RecordCount<Article>(querySql,queryObj);
                   ViewData["totalCount"]  = totalCount;
                   ViewData["totalPage"] = totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1);
                   ViewData["articleList"] = _connection.Query<Article>("select title,thumbnailUrl,latynUrl,addTime,author,shortDescription from article "+querySql + " order by addTime desc limit @start, @length ",queryObj)
                   .Select(x=>new Article(){
                     Title = x.Title,
                     ThumbnailUrl =  string.IsNullOrEmpty(x.ThumbnailUrl)?no_image:"https://infohub.kz"+x.ThumbnailUrl,
                     LatynUrl = x.LatynUrl,
                     AddTime = x.AddTime,
                     Author = x.Author,
                     ShortDescription = x.ShortDescription
                   }).ToList();
                }
               return View("~/Views/Home/ArticleList.cshtml"); 
         }else{
                 query = query.EndsWith(".html")?query.Substring(0,query.Length-5):query; //Latyn url
                 Article article = null;
                   using(var _connection = Utilities.GetOpenConnection())
                      {
                       article =  _connection.GetList<Article>("where qStatus = 0 and latynUrl = @latynUrl",new {latynUrl = query}).FirstOrDefault();
                       if(article == null) return Redirect("/404.html");
                    //    article.ViewCount+=1;
                    //    _connection.Update<Article>(article);
                       _connection.ExecuteAsync("update article set viewCount += 1 where id = @articleId",new {articleId = article.Id});
                     }
                 ViewData["article"] = article;
                 ViewData["latestArticleList"] =  ElCache.GetLatestArticleList(_memoryCache,3);

            return View("~/Views/Home/ArticleView.cshtml"); 
         }
     }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult UploadImage(string cropInfoStr, IFormFile imgFile)
    {
  
           CropInfoModel cropInfo = JsonHelper.DeserializeObject<CropInfoModel>(cropInfoStr);    
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string descFilePath =  desktopPath + $"/{DateTime.Now.ToString("yyyyMMddmmssfff")}.png";        
            using(MemoryStream stream = new MemoryStream())
            {
                   imgFile.CopyTo(stream);
                   ImageHelper.CutImage(stream,descFilePath,(int)cropInfo.X,(int)cropInfo.Y,(int)cropInfo.Width,(int)cropInfo.Height);
            }
            return MessageHelper.RedirectAjax("Hah haha a df","success","",null);
    }   


      [AllowAnonymous]
      public IActionResult Editor()
      {
         using(var _connection = Utilities.GetOpenConnection())
        {
                ViewData["articleList"] = _connection.GetList<Article>("where qStatus = 0").ToList();
        }
        return View();
      }

    [AllowAnonymous]
    public IActionResult Test()
    {
        // string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        // string filePath =  desktopPath + "/greencircle.png";
        // string descFilePath =  desktopPath + "/newgreencircle.png";        
     
        // ImageHelper.CutImage(filePath,descFilePath,16f/9f);

        //var cutInfo = ImageHelper.GetCutInfo(filePath,16f/9f);
        //ImageHelper.DrawCircle(filePath,500,500);
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult SaveInfo(string content)
    {
        if(string.IsNullOrWhiteSpace(content)) //empty, newline, tab
            return Content("Please enter content!");

         uint currentTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now);
        using(var _connection = Utilities.GetOpenConnection())
        {
            _connection.Insert<Article>(new Article(){
                    FullDescription = content,
                    AddTime  = currentTime,
                     UpdateTime = currentTime,
                     QStatus = 0
                });
        }
          return Content("Save successfully!");
    }


   [AllowAnonymous]
    public  IActionResult Index(string query)
    {
        // int number =  10;
        // App app = new App(new FileLogger());
        // //Dependency Injection => DI

        // app.SaveLog("Error content!");
    //    string filePath  = _webHostEnvironment.WebRootPath+"/404.html";
    //    if(System.IO.File.Exists(filePath)){
    //        string html = System.IO.File.ReadAllText(filePath);
    //        return Content(html,"text/html");
    //    }

         return View();
    }

    public IActionResult Alem()
    {
        return View();
    }


    public IActionResult Logout()
    {
         HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
         return Redirect("/home/login");
    }


    #region Recover +Recover()
    [AllowAnonymous]
    public IActionResult Recover()
    {
        ViewData["tockenKey"] = HttpContext.Request.Query["key"].ToString();
        return View();
    }
    #endregion

    #region Recover  +Recover(Person item)
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Recover(Person item)
    {
        if (string.IsNullOrWhiteSpace(item.Email))
            return MessageHelper.RedirectAjax("Please enter your email address!", "error", "", "email");

        if (!RegexHelper.IsEmail(item.Email))
            return MessageHelper.RedirectAjax("Please enter your email address!", "error", "", "email");
         string ip = GetIPAddress();
         uint currentTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now);
        using (var _connection = Utilities.GetOpenConnection())
        {
            int sendEmailSmsCount = _connection.RecordCount<Smssendlog>("where email = @email and sendTime > @sendTime", new {email = item.Email, sendTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now.AddHours(-2))});
            if(sendEmailSmsCount > 5) 
                    return MessageHelper.RedirectAjax("Try later!!!", "error", "", null);
          
            int sendIpSmsCount = _connection.RecordCount<Smssendlog>("where ip = @ip and sendTime > @sendTime", new {ip, sendTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now.AddHours(-2))});
            if(sendIpSmsCount > 20) 
                    return MessageHelper.RedirectAjax("Try later!!!", "error", "", null);

                Person person =  _connection.GetList<Person>("where qStatus = 0 and emailConfirm = 1 and email = @email", new {email = item.Email}).FirstOrDefault();
                if( person == null)
                       return MessageHelper.RedirectAjax("This email not registerd!", "error", "", "email");

                         string encryptKey = QarTokenHelper.Encrypt(new TokenInfoModel()
                        {
                            Id = person.Id,
                            Time = currentTime,
                            Type = "recover"
                        });

                        string link = $"http://localhost:5019/home/recover?key={encryptKey}";
                        if (!EmailHelper.SendHtmlEmail(item.Email, link, out string message))
                        {
                            return MessageHelper.RedirectAjax("Send email failed, please try again or contact administrator!", "error", "", null);
                        }
                        _connection.Insert<Smssendlog>(new Smssendlog(){
                             Email = item.Email,
                             Ip = ip,
                             SendTime = currentTime,
                        });    
                 return MessageHelper.RedirectAjax("Email susccessfully sent!", "success", "", "");
        }   
    }

    #endregion
  
    #region Update Password +UpdatePassword(string password, string passwordConfirm, string tockenKey)
    [HttpPost]
    [AllowAnonymous]
    public IActionResult UpdatePassword(string password, string passwordConfirm, string tockenKey)
    {
     if (string.IsNullOrWhiteSpace(password))
            return MessageHelper.RedirectAjax("Please enter your password!", "error", "", "password");

        if (password.Length < 6 || password.Length > 18)
            return MessageHelper.RedirectAjax("Password length keep 6~18 chars!", "error", "", "password");


        if (!password.Equals(passwordConfirm))
            return MessageHelper.RedirectAjax("Confirm new password!", "error", "", "passwordConfirm");
        
        if(string.IsNullOrEmpty(tockenKey))
                return MessageHelper.RedirectAjax("Tocken key incorrect!", "error", "", "passwordConfirm");

          TokenInfoModel model = QarTokenHelper.Decrypt(tockenKey);
           if(model==null)
              return MessageHelper.RedirectAjax("Tocken key incorrect!", "error", "", "passwordConfirm");
                     using (var _connection = Utilities.GetOpenConnection())
                    {
                          Person person = _connection.GetList<Person>("where qStatus = 0 and emailConfirm = 1 and id = @id", new { id = model.Id }).FirstOrDefault();
                          if(person ==null)
                               return MessageHelper.RedirectAjax("This account has been deleted!", "error", "", "");
                            person.Password = MD5Helper.PasswordMd5Encrypt(password);
                            person.UpdateTime= UnixTimeHelper.ConvertToUnixTime(DateTime.Now);
                            if(_connection.Update<Person>(person)>0)
                            {
                                uint last2Hour  = UnixTimeHelper.ConvertToUnixTime(DateTime.Now.AddHours(-2));

                                _connection.Execute("update personloginlog set qStatus = 1 where personId = @personId and time >  @last2Hour ",
                                new {personId = person.Id, last2Hour});
                                return MessageHelper.RedirectAjax("Password update successfully!", "success", "/home/login", "");
                            }
                               

                           return MessageHelper.RedirectAjax("Save failed!", "error", "", "");
                    }

    }
    #endregion

    #region Login +Login()
   [AllowAnonymous]
    public IActionResult Login()
    {
         if (HttpContext.User.Identity.IsAuthenticated)
         {
                return Redirect($"/home/index");
         }
        return View();
    }
    #endregion


    #region Login +Login(Person item, string remember)
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login(Person item, string remember)
    {
         if (string.IsNullOrWhiteSpace(item.Email))
            return MessageHelper.RedirectAjax("Please enter your email address!", "error", "", "email");

        if (!RegexHelper.IsEmail(item.Email))
            return MessageHelper.RedirectAjax("Please enter your email address!", "error", "", "email");
 
        if (string.IsNullOrWhiteSpace(item.Password))
            return MessageHelper.RedirectAjax("Please enter your password!", "error", "", "password");

        if (item.Password.Length < 6 || item.Password.Length > 18)
            return MessageHelper.RedirectAjax("Email or password incorrect!", "error", "", "");

        string ip = GetIPAddress();
        uint currentTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now);
         using (var _connection = Utilities.GetOpenConnection())
        {
             object queryObj = new {ip , time  = UnixTimeHelper.ConvertToUnixTime(DateTime.Now.AddHours(-3))};
             if(_connection.RecordCount<Personloginlog>("where qStatus = 0 and ip = @ip and time > @time ",queryObj ) > 100){
                  return MessageHelper.RedirectAjax("Try again later!!", "error", "", "");
             }
             
              Person person =  _connection.GetList<Person>("where qStatus =  0 and email = @email", new {email = item.Email}).FirstOrDefault();
              if(person == null)
              {
                    _connection.Insert<Personloginlog>(new Personloginlog(){
                        PersonId = 0,
                        Ip = ip,
                        Time = currentTime,
                        qStatus = 2
                    });
                    return MessageHelper.RedirectAjax("Email or password incorrect!", "error", "", "");
              }

             queryObj = new {ip , time  = UnixTimeHelper.ConvertToUnixTime(DateTime.Now.AddHours(-3)),personId = person.Id};
             if(_connection.RecordCount<Personloginlog>("where qStatus = 0 and personId = @personId and  time > @time ", queryObj) > 3){
                  return MessageHelper.RedirectAjax("Try again later!!", "error", "", "");
             }
              item.Password = MD5Helper.PasswordMd5Encrypt(item.Password);
              if(!person.Password.Equals(item.Password))
              {
                   _connection.Insert<Personloginlog>(new Personloginlog(){
                        PersonId = person.Id,
                        Ip = ip,
                        Time = currentTime,
                        qStatus = 3
                    });
                  return MessageHelper.RedirectAjax("Email or password incorrect!", "error", "", "");
              }

            var identity = new ClaimsIdentity("AccountLogin");
            identity.AddClaim(new Claim("RealName", person.Name));
            identity.AddClaim(new Claim("PersonId", person.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return MessageHelper.RedirectAjax("Login susccessfully!", "success", "/home/index", "");
        }
    }
    #endregion


    [AllowAnonymous]
    public IActionResult Register()
    {
          if (HttpContext.User.Identity.IsAuthenticated)
         {
                return Redirect($"/home/index");
         }
        string key = HttpContext.Request.Query["key"].ToString();
        if(!string.IsNullOrEmpty(key)){
            TokenInfoModel model = QarTokenHelper.Decrypt(key);
            uint last24Hour = UnixTimeHelper.ConvertToUnixTime(DateTime.Now.AddHours(-24));
            if(model != null && 
            model.Type.Equals("Register",StringComparison.OrdinalIgnoreCase)
            && model.Time > last24Hour)
            {
                     using (var _connection = Utilities.GetOpenConnection())
                    {
                          Person person = _connection.GetList<Person>("where qStatus = 0 and emailConfirm = 0 and id = @id", new { id = model.Id }).FirstOrDefault();
                          if(person!=null)
                          {
                             person.EmailConfirm = 1;
                             person.UpdateTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now);
                             if(_connection.Update<Person>(person) >0)
                             {
                                return Redirect("/home/login");
                             }
                          }
                    }
            }
        }
        return View();
    }
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Register(Person item, string passwordConfirm)
    {
        if (string.IsNullOrWhiteSpace(item.Email))
            return MessageHelper.RedirectAjax("Please enter your email address!", "error", "", "email");

        if (!RegexHelper.IsEmail(item.Email))
            return MessageHelper.RedirectAjax("Please enter your email address!", "error", "", "email");

        if (string.IsNullOrWhiteSpace(item.Name))
            return MessageHelper.RedirectAjax("Please enter your name!", "error", "", "name");


        if (string.IsNullOrWhiteSpace(item.Password))
            return MessageHelper.RedirectAjax("Please enter your password!", "error", "", "password");

        if (item.Password.Length < 6 || item.Password.Length > 18)
            return MessageHelper.RedirectAjax("Password length keep 6~18 chars!", "error", "", "password");


        if (!item.Password.Equals(passwordConfirm))
            return MessageHelper.RedirectAjax("Confirm new password!", "error", "", "passwordConfirm");

        item.Password = MD5Helper.PasswordMd5Encrypt(item.Password);
        uint currentTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now);
        using (var _connection = Utilities.GetOpenConnection())
        {
            Person person = _connection.GetList<Person>("where qStatus = 0 and emailConfirm = 1 and email = @email", new { email = item.Email }).FirstOrDefault();
            if (person != null && person.EmailConfirm == 1)
                return MessageHelper.RedirectAjax("This email already registered!", "error", "", null);

            string ip = GetIPAddress();
            int sendEmailSmsCount = _connection.RecordCount<Smssendlog>("where email = @email and sendTime > @sendTime", new {email = item.Email, sendTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now.AddHours(-2))});
            if(sendEmailSmsCount > 5) 
                    return MessageHelper.RedirectAjax("Try later!!!", "error", "", null);
                                int sendIpSmsCount = _connection.RecordCount<Smssendlog>("where ip = @ip and sendTime > @sendTime", new {ip, sendTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now.AddHours(-2))});
            if(sendIpSmsCount > 20) 
                    return MessageHelper.RedirectAjax("Try later!!!", "error", "", null);

            using (var tran = _connection.BeginTransaction())
            {
                try
                {
                    int? res = 0;
                    uint personId = 0;
                    if (person != null)
                    {
                        personId = person.Id;
                        person.Name = item.Name;
                        person.RegisterTime = currentTime;
                        person.UpdateTime = currentTime;
                        person.Password = item.Password;
                        res = _connection.Update<Person>(person);
                    }
                    else
                    {
                        res = _connection.Insert<Person>(new Person()
                        {
                            Name = item.Name,
                            Email = item.Email,
                            EmailConfirm = 0,
                            Password = item.Password,
                            RegisterTime = currentTime,
                            UpdateTime = currentTime,
                            QStatus = 0
                        });
                        personId = Convert.ToUInt32(res ?? 0);
                    }
                    if (res > 0)
                    {

                        string encryptKey = QarTokenHelper.Encrypt(new TokenInfoModel()
                        {
                            Id = personId,
                            Time = currentTime,
                            Type = "register"
                        });

                        string link = $"http://localhost:5019/home/register?key={encryptKey}";
                        if (!EmailHelper.SendHtmlEmail(item.Email, link, out string message))
                        {
                            return MessageHelper.RedirectAjax("Send email failed, please try again or contact administrator!", "error", "", null);
                        }

                        _connection.Insert<Smssendlog>(new Smssendlog(){
                             Email = item.Email,
                             Ip = ip,
                             SendTime = currentTime,
                        });
                    }
                    tran.Commit();
                    return MessageHelper.RedirectAjax("Success sent email!", "success", "/home/index", null);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return MessageHelper.RedirectAjax(ex.Message, "error", "", null);
                }
            }
        }
    }
}