

using System.Globalization;



// if(keyInfo.Key  ==  ConsoleKey.Enter){
//     Console.WriteLine("Enter Pressed!!");
// }else if(keyInfo.Key == ConsoleKey.Backspace){
//     Console.WriteLine("Enter not pressed");
// }else{

// }

// ConsoleKeyInfo  keyInfo = Console.ReadKey(true);
// while(keyInfo.Key != ConsoleKey.Z)
// {
// switch(keyInfo.Key)
// {
//      default: {
//         Console.WriteLine("Other key pressed!");
//     }break;
//     case ConsoleKey.A:
//     case ConsoleKey.B: {
//         Console.WriteLine("A or B");
//     } break;
//     case ConsoleKey.C: {
//         Console.WriteLine("C");
//     } break;
// }
//     Console.WriteLine("Press Z to stop");
//     keyInfo = Console.ReadKey(true);
// }

// int[] arr =  [1,2,3,4,5,6,7,8,9,10];

// for(int i = 0; i < arr.Length; i++)
// {
//     Console.WriteLine(arr.GetValue(i));
// }


// foreach(int number in arr){
//     Console.WriteLine(number.ToString());
// }

// int i = 0;
// hi:
// if( i < arr.Length){
//     Console.WriteLine(arr[i++].ToString());
//     goto hi;
// };

int number = 110; // select * form user emial = 'nurbol@gmail.com' or '1' = '1';



// int sum = Calc.Sum(10,10);
// Console.WriteLine(sum.ToString());


int number1 = 10;
int number2 = 20;

// int temp =  number1;
// number1 = number2;
// number2 = temp;
 
 //Clac.Swat(&number1, &number2);
// Calc.Swap(ref number1, ref number2);

// Console.WriteLine("Number1 = " + number1);
// Console.WriteLine("Number2 = " + number2);


// int[] arr =  [1,2,3,4,5,6,7,8,9,10];
// Calc.Update(arr.Clone() as int[]);
// for(int i = 0; i < arr.Length; i++)
// {
//     Console.WriteLine(arr.GetValue(i));
// }

double sum = 0;
double sum2 = 0;
double sumsum = Calc.Sum(1,2,3,out sum, out sum2);


if(int.TryParse("ss", out int res))
{

}
Console.WriteLine("sum = " + sum);
Console.WriteLine("sumsum = " + sumsum);

var sum33333 = (int mumber1,int number2) => { //Lambda
    Console.WriteLine("number1 = " + number1);
};


if(!Calc.SaveData(1,"Maymul",1111, out string message))
{
    Console.WriteLine(message);
    return;
}