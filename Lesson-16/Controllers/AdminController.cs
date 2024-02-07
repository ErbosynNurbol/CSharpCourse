using COMMON;
using Dapper;
using DBHelper;
using Lesson_16.DI_IOC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using MODEL;

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
            using(var _connection = Utilities.GetOpenConnection())
            { 
                ViewData["mediaInfoList"] =   _connection.GetList<Mediainfo>("where qStatus = 0 order by updateTime desc ").ToList();
            }
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
  [RequestSizeLimit(1024 * 1024 * 1024)]
  [DisableRequestTimeout]
  public IActionResult UploadMedias(List<IFormFile> mFiles)
  {
    string[] fileFormats = [".mp3", ".mp4",".pdf", ".png", ".txt", ".jpg", ".jpeg", "gif"];
    string[] contentTypes = ["image/jpeg", "application/pdf", "audio/mpeg", "video/mp4", "text/plain", "image/png", "image/gif"];
    if (mFiles == null)
    return Json(new {Error= "Please select a file!"});
    foreach (IFormFile mFile in mFiles)
    {
      if (!fileFormats.Any(x => mFile.FileName.EndsWith(x, StringComparison.OrdinalIgnoreCase)))
       return Json(new {Error= "File format error!" + "(" + mFile.FileName + ")"});
      if (!contentTypes.Any(x => mFile.ContentType.Equals(x, StringComparison.OrdinalIgnoreCase)))
             return Json(new {Error= "Content type error!" + "(" + mFile.FileName + ")"});
    }
    try
    {
      List<Mediainfo> uploadedFilePathList = new List<Mediainfo>();
      for (int i = 0; i < mFiles.Count; i++)
      {
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (i + 1) + Path.GetExtension(mFiles[i].FileName);
        string relativePath = (_environment.WebRootPath.EndsWith("/") ? "" : "/") + "uploads/" + fileName;
        string absolutePath = _environment.WebRootPath + relativePath;
        if (!Directory.Exists(Path.GetDirectoryName(absolutePath)))
        {
          Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
        }
        using (FileStream file = System.IO.File.OpenWrite(absolutePath))
        {
          mFiles[i].CopyTo(file);
        }
        uploadedFilePathList.Add(new Mediainfo()
        {
          FilePath = relativePath,
          FileSize = mFiles[i].Length
        });
      }

      uint currentTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now);
      using (var _connection = Utilities.GetOpenConnection())
      {
          using(var _tran = _connection.BeginTransaction())
          {
            try{
   for (int i = 0; i < uploadedFilePathList.Count; i++)
        {
          string absolutePath = _environment.WebRootPath + uploadedFilePathList[i].FilePath;
          string fileMD5 = MD5Helper.GetFileMd5(absolutePath);
          Mediainfo mediaInfo = _connection.GetList<Mediainfo>("where qStatus = 0 and fileMD5 = @fileMD5 ", new { fileMD5 }).FirstOrDefault();
          if (mediaInfo != null)
          {
            mediaInfo.UpdateTime = currentTime;
            mediaInfo.UseCount += 1;
            _connection.Update<Mediainfo>(mediaInfo);
            if (System.IO.File.Exists(absolutePath))
            {
              System.IO.File.Delete(absolutePath);
            }
            uploadedFilePathList[i].FilePath = mediaInfo.FilePath;
          }
          else
          {
            _connection.Insert<Mediainfo>(new Mediainfo()
            {
              FilePath = uploadedFilePathList[i].FilePath,
              FileMD5 = fileMD5,
              FileSize = uploadedFilePathList[i].FileSize,
              UseCount = 1,
              QStatus = 0,
              UploadTime = currentTime,
              UpdateTime = currentTime
            });
          }
        }
                _tran.Commit();
            }catch{
                _tran.Rollback();
            }
          }
      }
      return Json(new {OK= "Success"});
    }
    catch
    {
       return Json(new {Error= "Try later!"});
    }
  }
  #endregion


  [HttpPost]
  public IActionResult SetMediaStatus(uint key)
  {
     using (var _connection = Utilities.GetOpenConnection())
      {
         Mediainfo mediaInfo =  _connection.GetList<Mediainfo>("where qStatus = 0 and id = @key ", new {key}).FirstOrDefault();
        if(mediaInfo == null) 
            return Json(new {Error= "Try later!"});
       
          mediaInfo.UpdateTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now);
          mediaInfo.QStatus = 1;
          if(_connection.Update<Mediainfo>(mediaInfo)>0)
          {
                 string absolutePath = _environment.WebRootPath + mediaInfo.FilePath;
                 if(System.IO.File.Exists(absolutePath))
                 {
                    System.IO.File.Delete(absolutePath);
                 }
          }else{
                   return Json(new {Error= "Save field!"});
          }
          return Json(new {OK="success"});
      }
  }
}