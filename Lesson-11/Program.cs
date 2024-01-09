using MODEL;
using Lesson_11;

        List<Person> personList = new List<Person>();
        Random random = new Random();
        for (int i = 0; i < 100; i++)
        {
            personList.Add(new Person
            {
                Name = "Person " + (i + 1),
                Age = random.Next(18, 100), // Random age between 18 and 99
                IsAlive = random.Next(2) == 0, // Randomly true or false
                Salary = Convert.ToDecimal(random.NextDouble() * 100000) // Random salary up to 100,000
            });
        }

//Linq Select
// IEnumerable<Person> yountPersonList = from Person in personList 
//                                 where Person.Age > 18 && Person.Age <= 35
//                                 select Person;

//var yountPersonList = personList.Where(x=>x.Age > 18 && x.Age <= 35).ToList();

// Func<int,int> funcArea = Calc.Area;
// int s= funcArea();

// Func<double,double> funcArea = Calc.CircleArea;
// double s = funcArea(10.0);
// Console.WriteLine("S = " + s);

// Func<int,int,double> funcArea = Calc.RectangleArea;
// double s = funcArea(10,10);
// Console.WriteLine("S = " + s);

//Lambda
// Func<int,int,double> funcArea = (n1,n2) => {
//     return n1 * n2;
// };
// funcArea(10,20);

//Lambda 
// List<string> youngPersonList =  personList.Where(x=>{
//     int myAge  = x.Age+1;
//     return myAge>18 && myAge<35;
//     }).Select(x=>{return x.Name;}).ToList();

// foreach (string name in youngPersonList)
// {
//     Console.WriteLine($"PersonName = {name} ");
// }

// decimal allSalary  = personList.Sum(x=>x.Salary);

// int count  = personList.Count(x=>x.Age > 90);


// Action<string,string> action = (x,y)=> Console.WriteLine(x+y);
// action("Hello World!","Hahaha");
//Linq Lambda
//personList.ForEach(x => Console.WriteLine(x.Name));

var groupList = personList.GroupBy(x=>x.IsAlive).ToList();
foreach (var item in groupList)
{
    //Console.WriteLine(item.Key?"Osy Duniede!":"Ana Duinyede!");
    foreach(var person in item)
    {
        Console.WriteLine(person.Name + (item.Key?" Osy Duniede!":" Ana Duinyede!"));
    }
}