using COMMON;
using Lesson_16.DI_IOC;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson_16.Filters;

public class QarActionFilter : IActionFilter
{
    //
    public void OnActionExecuting(ActionExecutingContext context)
    {
       var controller = context.Controller as Controller;
       controller.ViewData["realName"] = context.HttpContext.User.Identity.RealName();
       CurrencyInfo cInfo = context.HttpContext.RequestServices.GetService<CurrencyInfo>();
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
        
        
    }
}