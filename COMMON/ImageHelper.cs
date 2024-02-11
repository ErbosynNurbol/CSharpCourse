using System.Net.Http.Headers;
using MimeKit.Encodings;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SkiaSharp;

namespace COMMON;


public class ImageHelper
{

   public static void CutImage(MemoryStream stream,string descPath, int left,int top, int width, int height)
    {
             using(SKBitmap srcBitmap  = SKBitmap.Decode(stream.ToArray()))
              using(SKBitmap descBitmap  = new SKBitmap(width,height))
              using(SKCanvas canvas = new SKCanvas(descBitmap))
              {
                    SKRect srcRect = new SKRect(left,top,+left+width,top+height);
                    SKRect descRect = new SKRect(0,0,width,height);
                     canvas.DrawBitmap(srcBitmap,srcRect,descRect);
                     string format = Path.GetExtension(descPath).ToLower();
                     SKEncodedImageFormat skFormat;
                     switch(format)
                     {
                        case ".png":{
                                skFormat = SKEncodedImageFormat.Png;            
                        }break;
                         case ".jpg":
                         case ".jpeg":{
                              skFormat = SKEncodedImageFormat.Jpeg; 
                        }break;
                        default:{
                            skFormat = SKEncodedImageFormat.Bmp; 
                        }break;
                     }
                     using(var image = SKImage.FromBitmap(descBitmap))
                     {
                         using(var data = image.Encode(skFormat,100))
                         using(FileStream descStream = File.OpenWrite(descPath))
                         data.SaveTo(descStream);
                     }
              }


    }
   


    
    public static void CutImage(string srcPath,string descPath, float ratio)
    {
        int left = 0;
        int top = 0;
        int width = 0;
        int height = 0;
         using(FileStream stream = File.OpenRead(srcPath))
         using(SKData sKData = SKData.Create(stream))
         using(SKImage skImage = SKImage.FromEncodedData(sKData))
         {
               float orginalRatio = skImage.Width*1f/skImage.Height;
               if(ratio > orginalRatio)//height
               {
                 width = skImage.Width;
                 height = Convert.ToInt32(width*9f/16f);
                 left = 0;
                 top =  (skImage.Height - height)/2;
               }else{ //width
                 height = skImage.Height;
                 width = Convert.ToInt32(height*16f/9f);
                 top = 0;
                 left = (skImage.Width - width)/2;
               }

               using(SKBitmap srcBitmap  = SKBitmap.FromImage(skImage)) //Source Image
               using(SKBitmap descBitmap  = new SKBitmap(width,height))
              using(SKCanvas canvas = new SKCanvas(descBitmap))
              {
                    SKRect srcRect = new SKRect(left,top,+left+width,top+height);
                    SKRect descRect = new SKRect(0,0,width,height);
                     canvas.DrawBitmap(srcBitmap,srcRect,descRect);
                     string format = Path.GetExtension(descPath).ToLower();
                     SKEncodedImageFormat skFormat;
                     switch(format)
                     {
                        case ".png":{
                                skFormat = SKEncodedImageFormat.Png;            
                        }break;
                         case ".jpg":
                         case ".jpeg":{
                              skFormat = SKEncodedImageFormat.Jpeg; 
                        }break;
                        default:{
                            skFormat = SKEncodedImageFormat.Bmp; 
                        }break;
                     }
                     using(var image = SKImage.FromBitmap(descBitmap))
                     {
                         using(var data = image.Encode(skFormat,100))
                         using(FileStream descStream = File.OpenWrite(descPath))
                         data.SaveTo(descStream);
                     }
              }

         }

    }
   


    #region Get Cut Info +GetCutInfo(string filePath,float ratio)
    public static (int x,int y,int width,int height) GetCutInfo(string filePath,float ratio)
    {
        int x = 0;
        int y = 0;
        int width = 0;
        int height = 0;
         using(FileStream stream = File.OpenRead(filePath))
         using(SKData sKData = SKData.Create(stream))
         using(SKImage skImage = SKImage.FromEncodedData(sKData))
         {
               float orginalRatio = skImage.Width*1f/skImage.Height;
               if(ratio > orginalRatio)//height
               {
                 width = skImage.Width;
                 height = Convert.ToInt32(width*9f/16f);
                 x = 0;
                 y =  (skImage.Height - height)/2;
               }else{ //width
                 height = skImage.Height;
                 width = Convert.ToInt32(height*16f/9f);
                 y = 0;
                 x = (skImage.Width - width)/2;
               }
         }
        return (x,y,width,height);
    }
    #endregion

    #region Draw Circle +DrawCircle(string filePath,int width, int height)
    public static void DrawCircle(string filePath,int width, int height)
    {
       using(SKBitmap sKBitmap = new SKBitmap(width,height))
       {
            using(SKCanvas canvas = new SKCanvas(sKBitmap))
            {
                canvas.Clear(SKColor.Parse("#00ff00"));//Red Green Blue
                SKPaint sKPaint = new SKPaint()
                {   
                    Color = SKColor.Parse("#0094ff"),
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 5
                };
                canvas.DrawCircle(width/2f,height/2f,height/4f,sKPaint);
            }

            using(var image = SKImage.FromBitmap(sKBitmap))
            using(var data = image.Encode(SKEncodedImageFormat.Png,100))
            using(FileStream stream = File.OpenWrite(filePath))
                  data.SaveTo(stream);
       }
    }
    #endregion

}