namespace Lesson_3.Helpers
{
    public class Teacher : Person //Base class
    {
       decimal Salary{get;set;}
       public Teacher(string name , decimal salary , string surname, string idNumber)
       :base(name, surname, idNumber)
       {
         Salary = salary;
       }
    }
}