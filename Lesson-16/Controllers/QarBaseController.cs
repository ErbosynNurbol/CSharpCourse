using COMMON;

namespace Lesson_16;

public class QarBaseController : Controller
{ 

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


}