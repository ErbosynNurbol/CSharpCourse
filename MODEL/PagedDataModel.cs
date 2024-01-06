namespace MODEL;

public class PagedDataModel
{
    public int Page{get;set;}
    public int PageSize{get;set;}
    public int Total{get;set;}
    public int TotalPage{get;set;}
    public object DataList{get;set;}
}