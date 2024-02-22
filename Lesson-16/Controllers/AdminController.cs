using COMMON;
using Dapper;
using DBHelper;
using Lesson_16.DI_IOC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.Caching.Memory;
using MODEL;

namespace Lesson_16;


[Authorize]
public class AdminController : QarBaseController
{

  IWebHostEnvironment _environment;
  public AdminController(IWebHostEnvironment environment,IMemoryCache memoryCache):base(memoryCache)
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

   public IActionResult Service(string query)
  {
     string language = (ViewData["language"]??string.Empty) as string;
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
          int serviceId = GetIntQueryParam("id",0);
          if(serviceId<=0) 
            return Redirect("/404.html");
            using(var _connection = Utilities.GetOpenConnection())
            { 
                 Service service =    _connection.GetList<Service>("where qStatus = 0 and id = @serviceId ", new {serviceId}).FirstOrDefault();
                 if(service == null)
                    return Redirect("/404.html");
                    
                 ViewData["service"] = service;
                 ViewData["multiLanguageList"] = GetMultiLanguageList(_connection,nameof(MODEL.Service),new List<uint>(){service.Id});
            }
        }
        break;
      case "list":
        {
                  int page = GetIntQueryParam("page",1);
                page = page<=0?1:page;
                int pageSize = GetIntQueryParam("pageSize",10);
                pageSize = pageSize<=0?10:pageSize;
                ViewData["page"] = page;
                ViewData["pageSize"] = pageSize;
                using(var _connection = Utilities.GetOpenConnection())
                {
                   string querySql = "where qStatus = 0 ";
                   object queryObj = new {start = (page-1)*pageSize,length = pageSize};
                   int totalCount = _connection.RecordCount<Service>(querySql,queryObj);
                   ViewData["totalCount"]  = totalCount;
                   ViewData["totalPage"] = totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1);
                   var serviceList = _connection.GetList<Service>(querySql + " order by addTime desc limit @start, @length ",queryObj).ToList();
                   if(serviceList.Count()>0)
                   {
                       List<Multilanguage> multiLanguageList = GetMultiLanguageList(_connection,nameof(MODEL.Service),serviceList.Select(x=>x.Id).ToList(),language,new List<string>(){"title","content"});
                      foreach(var service in serviceList)
                      {
                            var serviceTitle = multiLanguageList.FirstOrDefault(x=>x.ColumnId == service.Id && x.ColumnName.Equals("title"));
                            if(serviceTitle!=null)
                            {
                              service.Title = serviceTitle.ColumnValue;
                            }

                             var serviceContent = multiLanguageList.FirstOrDefault(x=>x.ColumnId == service.Id && x.ColumnName.Equals("content"));
                            if(serviceContent!=null)
                            {
                              service.Content = serviceContent.ColumnValue;
                            }
                      }
                 
                   }

                   ViewData["serviceList"] = serviceList;
            }
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


  [HttpPost]
  public IActionResult CreateOrEditService(Service item,string multiJsonStr)
  {
    string language = (ViewData["language"]??string.Empty) as string;
    item.Content = item.Content??string.Empty;
    if(string.IsNullOrEmpty(item.Title)||string.IsNullOrEmpty(item.Title = item.Title.Trim()))
        return MessageHelper.RedirectAjax(T("ls_Thisfieldisrequired"),"error","","title");
    if(item.Title.Length>255)
         return MessageHelper.RedirectAjax(T("ls_Thetextenteredexceedsthemaximumlength"),"error","","title");

    List<Multilanguage> multiLanguageList = new List<Multilanguage>();
    if(!string.IsNullOrEmpty(multiJsonStr)){
      try{
          multiLanguageList = JsonHelper.DeserializeObject<List<Multilanguage>>(multiJsonStr);
      }catch(Exception ex){
          Serilog.Log.Error(ex,"CreateOrEditService");
      }
    }
      uint currentTime = UnixTimeHelper.ConvertToUnixTime(DateTime.Now);
      int? result = null;
      using(var _connection = Utilities.GetOpenConnection())
      {
         if(item.DisplayOrder == 0){
             item.DisplayOrder =  _connection.Query<uint?>("select max(displayOrder) from service where qStatus = 0").FirstOrDefault()??0;
             item.DisplayOrder += 1;
          }

        if(item.Id == 0)
        {
          result =  _connection.Insert<Service>(new Service(){
                Title = item.Title,
                Content = item.Content,
                DisplayOrder = item.DisplayOrder,
                AddTime = currentTime,
                UpdateTime = currentTime,
                QStatus  = 0
            });
          if(result>0)
          {
                SaveMutliLanguage(_connection,multiLanguageList,nameof(MODEL.Service),Convert.ToUInt32(result));
                return MessageHelper.RedirectAjax(T("ls_SavedSuccessfully"),"success",$"/{language}/admin/service/list","");
          }
                
        }else{
            Service service =  _connection.GetList<Service>("where qStatus = 0 and id = @serviceId", new {serviceId = item.Id}).FirstOrDefault();
            if(service ==null)
                 return MessageHelper.RedirectAjax(T("ls_Isdeletedoridiswrong"),"error","","");
          service.Title = item.Title;
          service.Content = item.Content;
          service.DisplayOrder = item.DisplayOrder;
          service.UpdateTime = currentTime;
         result =  _connection.Update<Service>(service);
         if(result > 0)
         {
            SaveMutliLanguage(_connection,multiLanguageList,nameof(MODEL.Service),service.Id);
            return MessageHelper.RedirectAjax(T("ls_Updatesuccessfully"),"success",$"/{language}/admin/service/edit?id={service.Id}","");
         }
        }
      
          return MessageHelper.RedirectAjax(T("ls_Savefailed"),"error","","");
      }

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