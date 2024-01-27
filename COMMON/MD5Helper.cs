using System.Security.Cryptography;
using System.Text;

namespace COMMON;

public class MD5Helper
{

         private const string salt = "eloda.com";
          #region MD5 арқылы құпияластыру  +  CreateHashMD5(string s)
        /// <summary>
        /// MD5 арқылы құпияластыру
        /// </summary>
        /// <param name="s">Құпияластырмақшы болған сөз</param>
        /// <returns></returns>
        public static string CreateHashMD5(string s)
        {
            MD5 algorithm = MD5.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(s));
            string md5 = "";
            for (int i = 0; i < data.Length; i++)
            {
                md5 += data[i].ToString("x2").ToUpperInvariant();
            }
            return md5;
        }
        #endregion

        #region Құпия сөзді құпияластыру +PasswordMd5Encrypt(string password, string salt)
        /// <summary>
        /// Құпия сөзді құпияластыру
        /// </summary>
        /// <param name="password">Құпия сөз</param>
        /// <param name="salt">Тұздық</param>
        /// <returns></returns>
        public static string PasswordMd5Encrypt(string password)
        {
            password = CreateHashMD5(password);
            password = CreateHashMD5(password + salt);
            return password;
        } 
        #endregion

}