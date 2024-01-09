namespace Lesson_10;

public class Button
{
     public delegate void RoutedEventHandler(object sender); 
     public event RoutedEventHandler Click;

     public void OnClick()
     {
        Click.Invoke(this);
     }



}