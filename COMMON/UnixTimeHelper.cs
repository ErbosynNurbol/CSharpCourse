
using System.Globalization;

namespace COMMON
{
    public class UnixTimeHelper
    {
        public static uint ConvertToUnixTime(DateTime datetime)
        {
            return (uint)(datetime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static DateTime UnixTimeToDateTime(uint unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }

        public static string UnixTimeToStringFromNow(uint unixtime)
        {
            DateTime time = UnixTimeToDateTime(unixtime);
            TimeSpan span = DateTime.Now - time;
            //if (span.TotalDays > 365)
            //{
            //    return string.Format("{0} жыл бұрын", (int)(Math.Floor(span.TotalDays) / 365));
            //}
            //else if (span.TotalDays > 30)
            //{
            //    return string.Format("{0} ай бұрын", (int)(Math.Floor(span.TotalDays) / 30));
            //}
            if (span.TotalDays > 30)
            {
                return time.ToString("dd MMMM, yyyy HH:mm", new CultureInfo("kk-KZ"));
            }
            else if (span.TotalDays > 7)
            {
                return string.Format("{0} апта бұрын", (int)(Math.Floor(span.TotalDays) / 7));
            }
            else if (span.TotalDays > 1)
            {
                return string.Format("{0} күн бұрын", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return
                string.Format("{0} сағат бұрын", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return
                string.Format("{0} минут бұрын", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return
                string.Format("{0} секунд бұрын", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1 секунд бұрын";
            }
        }


        public static string AstanaUnixTimeToString(uint unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime astanaTime = sTime.AddSeconds(unixtime);
            astanaTime = DateTime.SpecifyKind(astanaTime, DateTimeKind.Local);
            string dateString = astanaTime.ToString("o");
            return dateString;
        }

        public static string UnixTimeToWeekLocalString(uint unixtime)
        {
            DateTime dateTime = UnixTimeToDateTime(unixtime);
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        return "ls_Monday";
                    }
                case DayOfWeek.Tuesday:
                    {
                        return "ls_Tuesday";
                    }
                case DayOfWeek.Wednesday:
                    {
                        return "ls_Wednesday";
                    }
                case DayOfWeek.Thursday:
                    {
                        return "ls_Thursday";
                    }
                case DayOfWeek.Friday:
                    {
                        return "ls_Friday";
                    }
                case DayOfWeek.Saturday:
                    {
                        return "ls_Saturday";
                    }
                case DayOfWeek.Sunday:
                    {
                        return "ls_Sunday";
                    }
                default:
                    {
                        return "";
                    }
            }

        }
    }
}
