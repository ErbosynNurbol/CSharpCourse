using Newtonsoft.Json;

namespace COMMON;
  public class JsonHelper
    {

        #region Өбиектіні Json ге озгерту  +SerializeObject(object obj)
        /// <summary>
        /// Өбиектіні Json ге озгерту
        /// </summary>
        /// <param name="obj">Өбиекті</param>
        /// <returns>json</returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        #endregion

        #region Json ды өбиектіге озгерту +DeserializeObject<T>(string str)
        /// <summary>
        /// Json ды өбиектіге озгерту
        /// </summary>
        /// <typeparam name="T">Өбиекті түрі</typeparam>
        /// <param name="str">JSON</param>
        /// <returns>Өбиекті</returns>
        public static T DeserializeObject<T>(string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch
            {
                return default;
            }
           
        }
        #endregion
    }