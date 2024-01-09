using Lesson_10;
using MODEL;



public class Program
{
    delegate void ShowMessage(string message);
    delegate int CalcSum(int number1, int number2);
    static event CalcSum evCalc;
    static void Main(string[] args)
    {
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

        // Msg msg = new();
        // ShowMessage showMessage = new ShowMessage(msg.Show);
        // showMessage+= msg.Display;
        // showMessage -= msg.Show;
        // showMessage("Hello World!");


        // CalcSum calcSum = new CalcSum(Calc.Add);
        // int sum = calcSum(10,20);
        // Console.WriteLine("Sum = " + sum);

    
    //      evCalc += Calc.Add;

    //    int? sum =  evCalc?.Invoke(10,20);
    //    Console.WriteLine("Sum = " + sum);

        // Button button = new Button();
        // button.Click += Btn_Click;
        // button.OnClick();

    
    }

    static void Btn_Click(object sender)
    {
        Console.WriteLine("Clicked!!!");
    }

}

       


//select name from person where age > 20 and age < 25;
//Linq

// IEnumerable<string> youngPersonNameList = from Person in personList 
//                                 where Person.Age > 20 && Person.Age < 25
//                                 select Person.Name;

// foreach (string name in youngPersonNameList)
// {
//     Console.WriteLine(name);
// }













