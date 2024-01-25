
namespace Lesson_15.Controllers;
public class AdminController : Controller
{
    public IActionResult Login()
    {
        string backUrl =  Request.Query["backUrl"].ToString();
        ViewData["backUrl"] = backUrl;
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password,string backUrl)
    {
            if(email.Equals("mauleta@gmail.com")&&password.Equals("12345678"))
            {
                    if(string.IsNullOrEmpty(backUrl))
                     return Redirect("/Home/Index");
                     else
                     return Redirect(backUrl);
            }
            return Content("Email or password is incorrect!");
    }
}