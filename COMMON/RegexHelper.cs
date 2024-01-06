using System.Text.RegularExpressions;

namespace COMMON;

public class RegexHelper
{

    public static bool IsPhone(string phoneNumber,out string phone)
    {
        //87003142857
        //+77003142857
        //+86138282828128
         phone = "";
         if(!Regex.IsMatch(phoneNumber,@"(\+77|87)\d{9}")){
            return false;
         }
         phone  = phoneNumber.StartsWith("8")?$"+7{phoneNumber.Substring(1)}":phoneNumber;
         return true;
    }

     public static bool IsEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}