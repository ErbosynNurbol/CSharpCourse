using COMMON;
using Lesson_16.Cache;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Caching.Memory;

namespace  Lesson_16;
    public abstract class SiteRazorPage<TModel> : RazorPage<TModel>
    {
     public  string T(string localKey)
    {
         string language =  (ViewData["language"]??string.Empty) as string;
         IMemoryCache _memoryCache = ViewContext.HttpContext.RequestServices.GetService<IMemoryCache>();
         return ElCache.GetLanguageValue(_memoryCache,localKey,language);
    }

}