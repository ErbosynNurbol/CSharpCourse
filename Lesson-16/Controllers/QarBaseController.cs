using COMMON;

namespace Lesson_16;

public class QarBaseController : Controller
{ 

    public  static readonly string no_image = "/img/no_image.png";
     public QarBaseController()
    {
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
}