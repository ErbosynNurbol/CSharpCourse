using COMMON;
using Lesson_16.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Lesson_16;

public class QarBaseController : Controller
{ 

    public  static readonly string no_image = "/img/no_image.png";
    IMemoryCache _memoryCache;
     public QarBaseController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

       #region  Қолданушының IP әдіресін алу +GetIPAddress()
          public string GetIPAddress()
        {
            string locationIP = HttpContext.Connection.RemoteIpAddress.ToString();
            if (HttpContext.Request.Headers["X-Real-IP"].Count() > 0)
            {
                locationIP = HttpContext.Request.Headers["X-Real-IP"];
            }
            return locationIP;
        }
        #endregion

        public uint GetPersonId()
        {
            return HttpContext.User.Identity.PersonId();
        }

     public int GetIntQueryParam(string paramName, int defaultValue = 0)
     {
         return int.TryParse(Request.Query[paramName].ToString(), out int paramValue)?paramValue:defaultValue;
     }

     public string GetStringQueryParam(string paramName)
     {
         return Request.Query[paramName].ToString();
     }

    public  string T(string localKey)
    {
         string language =  (ViewData["language"]??string.Empty) as string;
         return ElCache.GetLanguageValue(_memoryCache,localKey,language);
    }
}