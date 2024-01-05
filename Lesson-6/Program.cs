

using System.Text.RegularExpressions;


//Console.WriteLine("Please enter your phone number");
// EnterPhoneNumber:
// string phoneNumber  = Console.ReadLine();

// if(Regex.IsMatch(phoneNumber,@"(\+77|87)\d{9}",RegexOptions.Multiline))
// {
//     Console.WriteLine("Its a valid phone number");
// }else{
//      Console.WriteLine("Please enter a valid phone number");
//      goto EnterPhoneNumber;
// }



   Timer timer =  new Timer(async (Object o)=>{

    Console.WriteLine("================================================");
    string url = "https://mig.kz"; // Replace with your URL
        try
        {
            using (HttpClient client = new HttpClient())
            {
                 string htmlCode = await client.GetStringAsync(url);
                 string pattern = @">(\w{3,4})</td>\s*<td.*?>([\d.]+)<";
                 RegexOptions options = RegexOptions.Multiline;
        
        foreach (Match m in Regex.Matches(htmlCode, pattern, options))
        {
            Console.WriteLine($"{m.Groups[1].Value} => {m.Groups[2].Value}");
        }
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
   }, null, 0, 1*5*1000);


    Console.ReadKey();
       
