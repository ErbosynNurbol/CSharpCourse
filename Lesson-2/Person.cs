
//latyn or  _ 933
using System.Reflection.Metadata.Ecma335;

class Person
{
    //Contructor
    public Person()
    {

    }
    FileStream file { get; set; }
    public string BirthCity { get; set; } = "Kazakhstan";
    public string Name { get; set;}
    public string FullName { get; set; }
    public int Age { 
         get {
               
                return 19;
             }  
        }
    public DateTime BirthDate { get; set; }

    ~Person()
    {
        if(file != null) file.Dispose();
    }
}