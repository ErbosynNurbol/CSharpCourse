
using Lesson_15.Models;

namespace Lesson_15.Controllers;


public class HomeController : Controller
{
    public IActionResult Index()
    {
        Person  person = new Person();
        person.Name = "Erlan";
        person.Age = 18;
        //ViewData["person"] = person;
        //ViewBag.person = person;
        return View(new {Person = person,Title="Site Title"});
    }
    public IActionResult About()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Name()
    {
        return Content("");
    }

    [HttpPost]
    public IActionResult Info()
    {
        return Json(new {Name = "Nurbol",Age=22});
    }
}

