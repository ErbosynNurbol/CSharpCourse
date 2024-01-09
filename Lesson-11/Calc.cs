using System.Formats.Asn1;
using MODEL;

namespace Lesson_11;

public class Calc
{
    public static int Area()
    {
        return 0;
    }

    public static double CircleArea(double r)
    {
        return 3.14*r*r;
    }

     public static double RectangleArea(int width, int height)
    {
        return width*height;
    }

    public static bool Check(Person person)
    {
        return person.Age>18 && person.Age <= 35;
    }
}