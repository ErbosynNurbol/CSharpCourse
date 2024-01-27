using System.Net;
using MODEL.FormatModels;

namespace COMMON
{
   public class QarTokenHelper
    {
        public static string Encrypt(TokenInfoModel tokenInfo)
        {
            string tokenInfoStr = JsonHelper.SerializeObject(tokenInfo);
            string encryptKey = AESHelper.EncryptText(tokenInfoStr, "123456");
            encryptKey = AESHelper.Base64Encode(encryptKey);
            return WebUtility.UrlEncode(encryptKey);
        }
        public static TokenInfoModel Decrypt(string qarToken)
        {
            try
            {
                qarToken = WebUtility.UrlDecode(qarToken);
                qarToken = AESHelper.Base64Decode(qarToken);
                string decryptKey = AESHelper.DecryptText(qarToken, "123456");
                return JsonHelper.DeserializeObject<TokenInfoModel>(decryptKey);
            }
            catch
            {
                return null;
            }
        }
    }
}
