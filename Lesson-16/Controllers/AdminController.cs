using COMMON;
using Microsoft.AspNetCore.Authorization;

namespace Lesson_16;


[Authorize]
public class AdminController : QarBaseController
{
    public IActionResult Article(string query)
    {
        query = (query??string.Empty).ToLower();
        ViewData["query"] = query;
        ViewData["ip"] = GetIPAddress();
        uint personId = GetPersonId();
        switch(query)
        {
            case "create":{
              
            }break;
            case "edit":{

            } break;
            case "list":{

            } break;
        }
         return View();
    }
}