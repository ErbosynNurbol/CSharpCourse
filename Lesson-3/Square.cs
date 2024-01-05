
namespace Lesson_3
{
    public class Square: Shape
    {
        public  Square(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double Area()
        {
            return Width * Height;
        }
    }
}