namespace MODEL;

public partial class Person
{
    public uint Id { get; set; }
    public string Name { get; set;}
    public string Email{get;set;}
    public byte EmailConfirm { get; set; }
    public string Password { get; set; }    
    public uint RegisterTime { get; set; }
    public uint UpdateTime { get; set; }
    public byte QStatus { get; set; }
}