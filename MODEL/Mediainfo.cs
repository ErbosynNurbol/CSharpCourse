namespace MODEL;

public class Mediainfo
{
    public uint Id{get;set;}
    public string FilePath{get;set;}
    public string FileMD5{get;set;}
    public long FileSize{get;set;}
    public uint UseCount{get;set;}
    public uint UploadTime{get;set;}
    public uint UpdateTime{get;set;}
    public byte QStatus{get;set;}

}