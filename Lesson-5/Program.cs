//string String

using System.Text;



string text = "Hell World,  Hahaha!!";

string[] arr = text.Split(Environment.NewLine, StringSplitOptions.None);
//text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);



string str = "Hello "+System.Environment.NewLine+", world!";// \n => MacOS \r => Linux \r\n =>Windows


//Console.WriteLine("Contains: "+str.Contains("ello",StringComparison.OrdinalIgnoreCase));


string[] arr2 = new string[]{"Hello","World!","Well","Come","To","My","Chanel!"};

List<int> personId = new List<int>(){1,2,3,4,5,6};

string personIdArr = string.Join(",",personId);

// string querySql  =  @$"select * from person  where id in ({personIdArr}) }}";
// Console.WriteLine(querySql);



// string querySql = string.Format("select * from person  where id in ({0}) and age > {1}",new []{personIdArr,"15"});
//  Console.WriteLine(querySql);


// string text2  = "/Mac/Home/Desktop/New Text Document2.txt";
// Console.WriteLine(text2);

// int index  = text2.LastIndexOf("/")+1;
// int index2  = text2.LastIndexOf(".");
// int length = index2 - index;
// string fileName  = text2.Substring(index,length);
// Console.WriteLine(fileName);





//Console.WriteLine(str.Equals("Hello "+System.Environment.NewLine+", World!",StringComparison.OrdinalIgnoreCase));


// string hello =  "";
//         hello += "Hello";
//         hello += " ";
//         hello += "World!";

// Console.WriteLine("Hello 1:" + hello);

// StringBuilder stringBuilder = new StringBuilder();
// stringBuilder.Append("Hello");
// stringBuilder.Append(" ");
// stringBuilder.Append("World!");
// string hello2 = stringBuilder.ToString();

// Console.WriteLine("Hello 2:" + hello2);

// string[] arr = new string[3];
// arr[0] = "Hello";
// arr[1] = " ";
// arr[2] = "World!";

// string hello3 =  string.Concat(arr);

// Console.WriteLine("Hello 3:" + hello3);



string sss = " aria-describedby=\"qtip-1\">461.0</td>";


string myStr = " Hello   Kazakhstan! ".TrimEnd();
myStr = myStr.Replace("world","Kazakhstan",StringComparison.OrdinalIgnoreCase);
string[] strr =  myStr.Split();


