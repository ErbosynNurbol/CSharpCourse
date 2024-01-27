using Microsoft.AspNetCore.Mvc;
using  MODEL.FormatModels;

namespace COMMON;
 public class MessageHelper
    {
        #region  Json форматында мән қайтару + RedirectAjax(string message, string status, string backUrl, object data)
        public static IActionResult RedirectAjax(string message, string status, string backUrl, object data)
        {
            AjaxMsgModel ajax = new AjaxMsgModel()
            {
                Message = message,
                Status = status,
                BackUrl = backUrl,
                Data = data
            };
            JsonResult result = new JsonResult(ajax);
            return result;
        }
        #endregion
    }