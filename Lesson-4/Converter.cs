
using System.Text;

public abstract class Converter
{
       public  enum Sound
        {
            Vowel, //Дауысты дыбыс
            Consonant, //Дауыссыз дыбыс
            Unknown //Белгісіз
        }
   public readonly  string[] CyrlChars = { "А", "Ә", "Ə", "Б", "В", "Г", "Ғ", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Қ", "Л", "М", "Н", "Ң", "О", "Ө", "Ɵ", "П", "Р", "С", "Т", "У", "Ұ", "Ү", "Ф", "Х", "Һ", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "І", "Ь", "Э", "Ю", "Я", "-" };
    public abstract string Convert(string text);

        #region Жат кирлл әріптерін төл кирлларыпыне айналдыру +CopycatCyrlToOriginalCyrl(string cyrlText)
        public  string CopycatCyrlToOriginalCyrl(string cyrlText)
        {
            return new StringBuilder(cyrlText)
                .Replace("Ə", "Ә")
                .Replace("ə", "ә")
                .Replace("Ɵ", "Ө")
                .Replace("ɵ", "ө").ToString();
        }
        #endregion
}

//Convert.h
//Convert.cpp