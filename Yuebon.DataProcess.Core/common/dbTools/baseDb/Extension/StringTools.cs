using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension
{
    public class StringTools
    {
        public StringTools() { }

        ///   <summary>
        ///   给一个字符串进行MD5加密
        ///   </summary>
        ///   <param   name="strText">待加密字符串</param>
        ///   <returns>加密后的字符串</returns>
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(strText));
            return Encoding.Default.GetString(result);
        }

        /// <summary>
        /// HTMLEntitiesEncode（HTMLEntities编码）
        /// </summary>
        /// <param name="text">需要转换的html文本</param>
        /// <returns>HTMLEntities编码后的文本</returns>
        public static string HtmlEntitiesEncode(string text)
        {
            // 获取文本字符数组
            char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();
            // 初始化输出结果
            StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));
            foreach (char c in chars)
            {
                // 将指定的 Unicode 字符的值转换为等效的 32 位有符号整数
                int value = Convert.ToInt32(c);
                // 内码为127以下的字符为标准ASCII编码，不需要转换，否则做 &#{数字}; 方式转换
                if (value > 127)
                {
                    result.AppendFormat("&#{0};", value);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        #region 数据库字符串加密解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="codeName">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            string enstring = "";
            byte[] bytes = encode.GetBytes(source);
            try
            {
                enstring = Convert.ToBase64String(bytes);
            }
            catch
            {
                enstring = source;
            }
            return enstring;
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }



        #endregion


    }
}
