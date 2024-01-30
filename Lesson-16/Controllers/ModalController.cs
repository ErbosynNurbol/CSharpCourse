using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;

namespace Lesson_16;

[Authorize]
public class ModalController : Controller
{
    [AllowAnonymous]
    public IActionResult SendEmailMessage()
    {
        return View();
    }
}


