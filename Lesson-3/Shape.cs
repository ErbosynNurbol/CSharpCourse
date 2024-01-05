
 namespace Lesson_3
{
    public class Shape
    {

        public  double Pi{get;} = 3.14;
        public double Width{get;set;}
        public double Height{get;set;}
        public double Radius{get;set;}
        public double Angle{get;set;}
        public virtual double Area()
        {
            return 0;
        }
    }
}