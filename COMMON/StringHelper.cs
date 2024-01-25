namespace COMMON;

public class StringHelper
{
    public static string GetRandomString(int length)
    {
        if(length<=0) return "";
        Random random = new Random();
        string randomNumberString = "";
        for (int i = 0; i < length; i++)
        {
            randomNumberString += random.Next(0, 10).ToString();
        }
        return randomNumberString;
    }
}