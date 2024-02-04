using COMMON;
using Lesson_16.DI_IOC;
using Microsoft.AspNetCore.Authorization;

namespace Lesson_16;


[Authorize]
public class AdminController : QarBaseController
{
     CurrencyInfo _currencyInfo;
    public AdminController(CurrencyInfo currencyInfo)
    {
          _currencyInfo = currencyInfo;

    }
    public IActionResult Article(string query)
    {
      var cInfo  = _currencyInfo;

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