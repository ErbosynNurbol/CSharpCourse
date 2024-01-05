
namespace Lesson_3.Helpers
{
    public class Student : Person
    {
        public string StudentId{get;}
        public Student(string name,string surname, string idNumber,string studentId):base(name,surname,idNumber)
        {
            StudentId  = studentId;
        }

    }
}