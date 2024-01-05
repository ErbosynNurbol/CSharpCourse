
namespace Lesson_3
{
    public class Circle : Shape 
    {
        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double Area ()
        {
            return Pi*Radius*Radius;
        }


    }
}