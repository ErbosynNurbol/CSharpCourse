using COMMON;
using Lesson_16.DI_IOC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;

namespace Lesson_16;


[Authorize]
public class AdminController : QarBaseController
{

  IWebHostEnvironment _environment;
  public AdminController(IWebHostEnvironment environment)
  {
    _environment = environment;
  }
  public IActionResult Article(string query)
  {
    query = (query ?? string.Empty).ToLower();
    ViewData["query"] = query;
    ViewData["ip"] = GetIPAddress();
    uint personId = GetPersonId();
    switch (query)
    {
      case "create":
        {

        }
        break;
      case "edit":
        {

        }
        break;
      case "list":
        {

        }
        break;
    }
    return View();
  }

  public IActionResult Media(string query)
  {
    query = (query ?? string.Empty).ToLower();
    ViewData["query"] = query;
    ViewData["ip"] = GetIPAddress();
    uint personId = GetPersonId();
    switch (query)
    {
      case "create":
        {

        }
        break;
      case "edit":
        {

        }
        break;
      case "list":
        {

        }
        break;
    }
    return View();
  }

  #region Upload Media +UploadMedia(IFormFile mFile)


  [HttpPost]
  [RequestSizeLimit(1024*1024*1024)]
  [DisableRequestTimeout]
  public IActionResult UploadMedias(List<IFormFile> mFiles)
  {
    string[] fileFormats = [".mp3", ".mp4", ".png", ".txt", ".jpg", ".jpeg", "gif"];
    string[] contentTypes = ["image/jpeg", "application/pdf", "audio/mpeg", "video/mp4", "text/plain", "image/png", "image/gif"];
    if(mFiles==null)
            return MessageHelper.RedirectAjax("Please select a file!", "error", "", null);
    foreach (IFormFile mFile in mFiles)
    {
      if (!fileFormats.Any(x => mFile.FileName.EndsWith(x, StringComparison.OrdinalIgnoreCase)))
        return MessageHelper.RedirectAjax("File format error!" + "(" + mFile.FileName + ")", "error", "", null);
      if (!contentTypes.Any(x => mFile.ContentType.Equals(x, StringComparison.OrdinalIgnoreCase)))
        return MessageHelper.RedirectAjax("Content type error!" + "(" + mFile.FileName + ")", "error", "", null);
    }
    try
    {
      List<string> uploadedFilePath = new List<string>();
      for(int i=0;i<mFiles.Count;i++)
      {
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff")+(i+1) + Path.GetExtension(mFiles[i].FileName);
        string relativePath = (_environment.WebRootPath.EndsWith("/") ? "" : "/") + "uploads/" + fileName;
        string filePath = _environment.WebRootPath + relativePath;
        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
          Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }
        using (FileStream file = System.IO.File.OpenWrite(filePath))
        {
          mFiles[i].CopyTo(file);
        }
        uploadedFilePath.Add(relativePath);
      }
      return MessageHelper.RedirectAjax("Upload Successfully!", "success", "", uploadedFilePath);
    }
    catch (Exception ex)
    {
      return MessageHelper.RedirectAjax("Try later!", "error", "", new { path = "" });
    }
  }
  #endregion
}