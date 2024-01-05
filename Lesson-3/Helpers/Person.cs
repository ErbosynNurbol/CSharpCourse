namespace Lesson_3.Helpers
{
    public class Person
    {
            public string Name { get; }

            public string Surname { get; }

            public string IdNumber { get; }

        public Person(string name, string surname, string  idNumber)
        {
            Name = name;
            Surname = surname;
            IdNumber = idNumber;
        }

    }
}