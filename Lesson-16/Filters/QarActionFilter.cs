using System.Text;
using COMMON;
using Lesson_16.DI_IOC;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Lesson_16.Filters;

public class QarActionFilter : IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
       var controller = context.Controller as Controller;
       controller.ViewData["language"] =  (string)context.RouteData.Values["culture"];
       controller.ViewData["realName"] = context.HttpContext.User.Identity.RealName();
       

    }


    public void OnActionExecuted(ActionExecutedContext context)
    {
               var controller = context.Controller as Controller;
               var language = (controller.ViewData["language"]??string.Empty) as string;
                IActionResult actionResult = context.Result;
                 string path = context.HttpContext.Request.Path.ToString().ToLower();
                string qUrl = ""; //context.HttpContext.Request.Scheme + "://" + language + "." + qHost;
                if (actionResult is ViewResult && (language.Equals("latyn") || language.Equals("tote"))) //If Html
                {
                      ViewResult viewResult = actionResult as ViewResult;
                      var services = context.HttpContext.RequestServices;
                        var executor = services.GetRequiredService<IActionResultExecutor<ViewResult>>() as ViewResultExecutor;
                        var option = services.GetRequiredService<IOptions<MvcViewOptions>>();
                        var result = executor.FindView(context, viewResult);
                        result.EnsureSuccessful(originalLocations: null);
                        var view = result.View;
                        StringBuilder builder = new StringBuilder();

                        using (var writer = new StringWriter(builder))
                        {
                            var viewContext = new ViewContext(
                                context,
                                view,
                                viewResult.ViewData,
                                viewResult.TempData,
                                writer,
                                option.Value.HtmlHelperOptions);

                            view.RenderAsync(viewContext).GetAwaiter().GetResult();
                            writer.Flush();
                        }
                        string html = builder.ToString();
                        StringValues sValue = string.Empty;
                        string userAgent = "pc";
                        html = HtmlAgilityPackHelper.ConvertHtmlTextNode(html, language, userAgent, qUrl);
                        ContentResult contentresult = new ContentResult();
                        contentresult.Content = html;
                        contentresult.ContentType = "text/html";
                        context.Result = contentresult;
                }
                else if (actionResult is JsonResult)
                {
                    JsonResult jsonResult = actionResult as JsonResult;
                    if (language.Equals("latyn") || language.Equals("tote"))
                    {
                        MODEL.FormatModels.AjaxMsgModel model = jsonResult.Value as MODEL.FormatModels.AjaxMsgModel;
                        if (model != null)
                        {
                            switch (language)
                            {
                                case "latyn": { model.Message = ConvertHelper.Cyrl2Latyn(model.Message); } break;
                                case "tote": { model.Message = Cyrl2ToteHelper.Cyrl2Tote(model.Message); } break;
                            }
                            jsonResult.Value = model;
                        }
                    }
                }
          
    }
}