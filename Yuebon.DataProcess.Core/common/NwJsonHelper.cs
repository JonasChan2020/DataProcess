using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Yuebon.DataProcess.Core.common
{

    /// <summary>
    /// JSON序列化、反序列化扩展类。
    /// </summary>
    public static class NwJsonHelper
    {
        public static object NwToJson(this string Json)
        {
            return string.IsNullOrEmpty(Json) ? null : JsonConvert.DeserializeObject(Json);
        }
        public static string NwToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }


        public static string NwToJson(this DataRow dr)
        {

            string jsonStr = "{";
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                string valStr = "";
                if (dr[dr.Table.Columns[i].ColumnName] != null)
                {
                    valStr = dr[dr.Table.Columns[i].ColumnName].ToString().Replace(":", "-").Replace(" ", "_");
                }
                if (jsonStr == "{")
                {
                    jsonStr += "'" + dr.Table.Columns[i].ColumnName + "':'" + valStr + "'";
                }
                else
                {
                    jsonStr += "," + "'" + dr.Table.Columns[i].ColumnName + "':'" + valStr + "'";
                }
            }
            jsonStr = jsonStr += "}";
            return jsonStr;
        }
        public static string NwToJson(this object obj, string datetimeformats)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }


        public static T NwToObject<T>(this string Json)
        {
            return string.IsNullOrEmpty(Json) ? default(T) : JsonConvert.DeserializeObject<T>(Json.Replace("&nbsp;", ""));
        }
        public static List<T> NwToList<T>(this string Json)
        {
            return string.IsNullOrEmpty(Json) ? null : JsonConvert.DeserializeObject<List<T>>(Json);
        }
        /// <summary>  
        /// 将json数据转换成实体类     
        /// </summary>  
        /// <returns></returns>  
        public static List<T> getObjectByJson<T>(this string jsonString)
        {
            // 实例化DataContractJsonSerializer对象，需要待序列化的对象类型  
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<T>));
            //把Json传入内存流中保存  
            //jsonString = "[" + jsonString + "]";
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            // 使用ReadObject方法反序列化成对象  
            object ob = serializer.ReadObject(stream);
            List<T> ls = (List<T>)ob;
            return ls;
        }

        public static DataTable NwToTable(this string Json)
        {
            return string.IsNullOrEmpty(Json) ? null : JsonConvert.DeserializeObject<DataTable>(Json);
        }
        public static JObject NwToJObject(this string Json)
        {
            return string.IsNullOrEmpty(Json) ? JObject.Parse("{}") : JObject.Parse(Json.Replace("&nbsp;", ""));
        }
        public static string NwGetJsonKeys(this string json)
        {
            string result = "";
            JObject obj = json.NwToJObject();
            foreach (JProperty jProperty in obj.Properties())
            {
                if (string.IsNullOrEmpty(result))
                {
                    result += jProperty.Name;
                }
                else
                {
                    result += "#$#" + jProperty.Name;
                }

            }
            return result;
        }
        public static string NwGetValue(this string json, string key)
        {
            return NwEachFindValue(json, key);
        }

        private static string NwEachFindValue(string json, string key)
        {
            string result = "";
            JObject obj = json.NwToJObject();
            if (obj.Property("j" + key) != null)
            {
                result = obj["j" + key].ToString();
            }
            else
            {
                foreach (JProperty jProperty in obj.Properties())
                {
                    if (jProperty.Name.Contains("j"))
                    {
                        string tmp = NwEachFindValue(jProperty.Value.ToString(), key);
                        if (!string.IsNullOrEmpty(tmp))
                        {
                            return tmp;
                        }
                    }

                }
            }
            return result;
        }
        public static String NwStringToJson(this string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                switch (c)
                {

                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
