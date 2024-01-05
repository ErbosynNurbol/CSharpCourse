
public class Cyrl2Latyn: Converter
{
    override public string Convert(string text)
    {
        text = CopycatCyrlToOriginalCyrl(text);


        return text;
    }

}