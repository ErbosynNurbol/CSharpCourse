using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Lesson_16.Helpers;
using Microsoft.AspNetCore.Http;
using COMMON;
using MODEL;

//yafo jszp qewe vjry

namespace Lesson_16.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Alem()
    {
        return View();
    }
    
    public IActionResult Login()
    {

        return View();
    }

     public IActionResult Register()
    {
       // ViewData["email"] =  HttpContext.Session.GetString("email");

        //   string time =   HttpContext.Session.GetString("time");
        // if(!string.IsNullOrEmpty(time))
        // {
        //     DateTime sendEmailTime = DateTime.ParseExact(time,"yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture);
        //     int timeLeft = 60 - Convert.ToInt32((DateTime.Now  - sendEmailTime).TotalSeconds);
        //     if(timeLeft<=0){
        //         HttpContext.Session.Remove("email");
        //         HttpContext.Session.Remove("time");
        //     }
        //     ViewData["timeLeft"] = timeLeft;
        // }

         if(Request.Cookies.TryGetValue("email",out string email) && Request.Cookies.TryGetValue("time",out string time))
         {
            email = AESHelper.DecryptText(email,"123456");
            DateTime sendEmailTime = DateTime.ParseExact(time,"yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture);
            int timeLeft = 60 - Convert.ToInt32((DateTime.Now  - sendEmailTime).TotalSeconds);
            if(timeLeft<=0){
                Response.Cookies.Delete("email");
                Response.Cookies.Delete("time");
            }
            ViewData["timeLeft"] = timeLeft;
            ViewData["email"] = email;
         }
        return View();
    }

    [HttpPost]
    public IActionResult GetVerifyCode(string email)
    {
        if(string.IsNullOrWhiteSpace(email)) 
        return Json(new {
            Status = "error",
            Message = "Please enter your email address!"
        });
        if(!RegexHelper.IsEmail(email))
         return Json(new {
            Status = "error",
            Message = "Please enter valid email address!"
        });
        
      
        string code  = StringHelper.GetRandomString(6);
        if(!EmailHelper.SendHtmlEmail("ErbosynNurbol@gmail.com",code,out string message))
         {
              return Json(new {
                  Status = "error",
                  Message = "Send email failed, please try again or contact administrator!"
             });
        }
        // HttpContext.Session.SetString("email",email);
           // HttpContext.Session.SetString("time",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.HttpOnly = true;
            //cookieOptions.Domain = "www.elorda.com";
            //4kb

            cookieOptions.IsEssential = true;
            email = AESHelper.EncryptText(email,"123456");
            Response.Cookies.Append("email",email,cookieOptions);
            Response.Cookies.Append("time",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),cookieOptions);

        return Json(new {
            Status = "success",
            Message = "Verify code is sent!"
        });
    }
}