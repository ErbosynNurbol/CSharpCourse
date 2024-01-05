


public class Cyrl2Tote: Converter
{
    override public string Convert(string text)
    { 
    
      text = CopycatCyrlToOriginalCyrl(text);
      string[] arr =  text.Split();
      foreach(string c in arr)
      {
        
      }
        


        return text;
    }

    public int Exqute(string sql)
    {
        throw new NotImplementedException();
    }

    public List<int> GetIdList()
    {
        throw new NotImplementedException();
    }

    public List<string> GetList(int id)
    {
        throw new NotImplementedException();
    }
}