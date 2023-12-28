
using System.Runtime.InteropServices.Marshalling;

public class Calc
{
    // public static int Sum(int number1, int number2)
    // {
    //     return number1 + number2;
    // }
    public static int Sum(int number1, int number2, int number3 = 20)
    {
        return number1 + number2+ number3;
    }
     public static double Sum(double number1, double number2, double number3, out double sum,out double sum2)
    {
        sum = number1 + number3;
        sum2 = number1 + number2;
        return number1 + number2+ number3;
    }

    public static void Swap(ref int num1,ref int num2)
    {
        int temp = num1;
        num1 = num2;
        num2 = temp;
    }

    public static void Update(int[] numbers){
        for(int i = 0;i < numbers.Length;i++){
            numbers[i] = numbers[i]+500;
        }
    }


    public static bool SaveData(int userId, string userName,int age, out string message)
    {
        //insert data into  database            

        //data saved in database

        message = "age is very big!";
        return false;
    }

    
    
}