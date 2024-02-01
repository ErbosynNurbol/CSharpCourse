using COMMON;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson_16.Filters;

public class QarActionFilter : IActionFilter
{
    //
    public void OnActionExecuting(ActionExecutingContext context)
    {
       var controller = context.Controller as Controller;
       controller.ViewData["realName"] = context.HttpContext.User.Identity.RealName();
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        int i = 1;
        
    }
}