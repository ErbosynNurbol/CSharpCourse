using System.Runtime.InteropServices.Marshalling;

namespace Lesson_16.DI_IOC;

public class Mator : IMator
{
   public void Start()
   {
         Console.WriteLine("Default Startting....");
   }
}