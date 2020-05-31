/*CLR Version: 4.0.30319.18063
 * Creat Date: 2016/6/27 10:26:02
 * Creat Year: 2016
 * Creator: 程炜.Snail
 */
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http; //.netFramework 4.0
using System.Web;  // .netFramework 4.0
namespace Utility.Http
{
    /// <summary>
    /// 自定义HTTP客户端
    /// </summary>
    public class HttpClient
    {
        /// <summary>
        /// 根据URL获取返回数据
        /// </summary>
        /// <returns></returns>
        public static string Get(string uri, string encoding = "GB2312")
        {
            string strBuff = string.Empty;
            Uri httpURL = new Uri(uri);
            try
            {
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                using (Stream respStream = httpResp.GetResponseStream())
                {
                    using (StreamReader respStreamReader = new StreamReader(respStream, Encoding.GetEncoding(encoding)))
                    {
                        strBuff = respStreamReader.ReadToEnd();
                    }
                }
                httpResp.Close();
                httpReq.Abort();
            }
            catch (Exception ex)
            {
                strBuff = string.Empty;
            }
            return strBuff;
        }
        /// <summary>
        /// POST发送数据到指定[键值对 a=1&amp;c=1]Url
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="postData">需要发送的数据</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Post(string uri, string postData, string encoding = "GB2312")
        {
            string strBuff = string.Empty;
            byte[] byteArray = Encoding.GetEncoding(encoding).GetBytes(postData);
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                webRequest.Method = "post";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                System.IO.Stream newStream = webRequest.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                using (Stream respStream = response.GetResponseStream())
                {
                    using (StreamReader respStreamReader = new StreamReader(respStream, Encoding.GetEncoding(encoding)))
                    {
                        strBuff = respStreamReader.ReadToEnd();
                    }
                }
                response.Close();
                webRequest.Abort();
            }
            catch (Exception ex)
            {
                strBuff = string.Empty;
            }
            return strBuff;
        }
        /// <summary>
        /// POST发送数据到指定Url
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="data">动态类型,传入Class类型的对象,自动转url参数</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Post(string uri, dynamic data, string encoding = "GB2312")
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder = GetParameter(strBuilder, data);
            return Post(uri, strBuilder.ToString(), encoding);
        }

        /// <summary>
        /// POST发送数据到指定[会将对象数据转换成json数据进行发送]Url
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="url">post地址</param>
        /// <param name="data">参数</param>
        /// <returns>返回Json数据</returns>
        public static string Post<T>(string url, T data)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsJsonAsync<T>(url, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        private static StringBuilder GetParameter(StringBuilder strBuilder, dynamic data)
        {
            Type t = data.GetType();  //匿名对象的使用, 需要引用 Microsoft.CSharp 程序集(.netFramework 4.0)
            if (typeof(string) == t)
            {
                return new StringBuilder(data);
            }
            // 获取类的所有公共属性
            System.Reflection.PropertyInfo[] pInfo = t.GetProperties();
            // 遍历公共属性
            foreach (System.Reflection.PropertyInfo pio in pInfo)
            {
                if (strBuilder.Length > 0)
                {
                    strBuilder.Append("&");
                }
                string fieldName = pio.Name;        // 公共属性的Name
                Type pioType = pio.PropertyType;    // 公共属性的类型
                var v = pio.GetValue(data);
                string value = string.Empty;

                if ((pioType == typeof(String))
                        || (pioType == typeof(int) || pioType == typeof(int?) || pioType == typeof(Nullable<int>))
                        || (pioType == typeof(double) || pioType == typeof(double?) || pioType == typeof(Nullable<double>))
                        || (pioType == typeof(decimal) || pioType == typeof(decimal?) || pioType == typeof(Nullable<decimal>))
                        || (pioType == typeof(short) || pioType == typeof(short?) || pioType == typeof(Nullable<short>))
                        || (pioType == typeof(byte) || pioType == typeof(byte?) || pioType == typeof(Nullable<byte>))
                        || (pioType == typeof(long) || pioType == typeof(long?) || pioType == typeof(Nullable<long>)))
                {
                    value = Convert.ToString(v);
                }
                else if ((pioType == typeof(DateTime) || pioType == typeof(DateTime?) || pioType == typeof(Nullable<DateTime>)))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(v)))
                    {
                        value = "";
                    }
                    else
                    {
                        value = Convert.ToDateTime(v).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                else
                {
                    try
                    {
                        strBuilder = GetParameter(strBuilder, v);
                    }
                    catch (Exception)
                    { }
                    continue;
                }
                strBuilder.AppendFormat("{0}={1}", fieldName, HttpUtility.UrlEncode(value));
            }
            return strBuilder;
        }
    }
}
