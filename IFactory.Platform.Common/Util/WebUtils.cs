using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IFactory.Platform.Common.Util
{
    public sealed class WebUtils
    {
        private int _timeout = 100000;

        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public string DoPost(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest webRequest = GetWebRequest(url, "POST");
            string str = "application/x-www-form-urlencoded;charset=utf-8";
            webRequest.ContentType = str;
            byte[] bytes = Encoding.UTF8.GetBytes(BuildQuery(parameters));
            Stream requestStream = webRequest.GetRequestStream();
            byte[] buffer = bytes;
            int offset = 0;
            int length = bytes.Length;
            requestStream.Write(buffer, offset, length);
            requestStream.Close();
            HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        public async Task<string> DoPostAsync(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest req = GetWebRequest(url, "POST");
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters));
            Stream requestStreamAsync = await req.GetRequestStreamAsync();
            byte[] buffer = postData;
            int offset = 0;
            int length = postData.Length;
            requestStreamAsync.Write(buffer, offset, length);
            requestStreamAsync.Close();
            HttpWebResponse rsp = (HttpWebResponse)await req.GetResponseAsync();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return await GetResponseAsStringAsync(rsp, encoding);
        }

        public string DoGet(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
                url = !url.Contains("?") ? url + "?" + BuildQuery(parameters) : url + "&" + BuildQuery(parameters);
            HttpWebRequest webRequest = GetWebRequest(url, "GET");
            string str = "application/x-www-form-urlencoded;charset=utf-8";
            webRequest.ContentType = str;
            HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        public string DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, FileItem> fileParams)
        {
            if (fileParams == null || fileParams.Count == 0)
                return DoPost(url, textParams);
            string @string = DateTime.Now.Ticks.ToString("X");
            HttpWebRequest webRequest = GetWebRequest(url, "POST");
            webRequest.ContentType = "multipart/form-data;charset=utf-8;boundary=" + @string;
            Stream requestStream = webRequest.GetRequestStream();
            byte[] bytes1 = Encoding.UTF8.GetBytes("\r\n--" + @string + "\r\n");
            byte[] bytes2 = Encoding.UTF8.GetBytes("\r\n--" + @string + "--\r\n");
            string str1 = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            IEnumerator<KeyValuePair<string, string>> enumerator1 = textParams.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                string format = str1;
                KeyValuePair<string, string> current = enumerator1.Current;
                string key = current.Key;
                current = enumerator1.Current;
                string str2 = current.Value;
                byte[] bytes3 = Encoding.UTF8.GetBytes(string.Format(format, key, str2));
                requestStream.Write(bytes1, 0, bytes1.Length);
                requestStream.Write(bytes3, 0, bytes3.Length);
            }
            string format1 = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            IEnumerator<KeyValuePair<string, FileItem>> enumerator2 = fileParams.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                KeyValuePair<string, FileItem> current = enumerator2.Current;
                string key = current.Key;
                current = enumerator2.Current;
                FileItem fileItem = current.Value;
                byte[] bytes3 = Encoding.UTF8.GetBytes(string.Format(format1, key, fileItem.GetFileName(), fileItem.GetMimeType()));
                requestStream.Write(bytes1, 0, bytes1.Length);
                requestStream.Write(bytes3, 0, bytes3.Length);
                byte[] content = fileItem.GetContent();
                requestStream.Write(content, 0, content.Length);
            }
            requestStream.Write(bytes2, 0, bytes2.Length);
            requestStream.Close();
            HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        public async Task<string> DoPostAsync(string url, IDictionary<string, string> textParams, IDictionary<string, FileItem> fileParams)
        {
            if (fileParams == null || fileParams.Count == 0)
                return DoPost(url, textParams);
            string boundary = DateTime.Now.Ticks.ToString("X");
            HttpWebRequest req = GetWebRequest(url, "POST");
            req.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            Stream requestStreamAsync = await req.GetRequestStreamAsync();
            byte[] bytes1 = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] bytes2 = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            string str1 = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            IEnumerator<KeyValuePair<string, string>> enumerator1 = textParams.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                string format = str1;
                KeyValuePair<string, string> current = enumerator1.Current;
                string key = current.Key;
                current = enumerator1.Current;
                string str2 = current.Value;
                byte[] bytes3 = Encoding.UTF8.GetBytes(string.Format(format, key, str2));
                requestStreamAsync.Write(bytes1, 0, bytes1.Length);
                requestStreamAsync.Write(bytes3, 0, bytes3.Length);
            }
            string format1 = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            IEnumerator<KeyValuePair<string, FileItem>> enumerator2 = fileParams.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                string key = enumerator2.Current.Key;
                FileItem fileItem = enumerator2.Current.Value;
                byte[] bytes3 = Encoding.UTF8.GetBytes(string.Format(format1, key, fileItem.GetFileName(), fileItem.GetMimeType()));
                requestStreamAsync.Write(bytes1, 0, bytes1.Length);
                requestStreamAsync.Write(bytes3, 0, bytes3.Length);
                byte[] content = fileItem.GetContent();
                requestStreamAsync.Write(content, 0, content.Length);
            }
            requestStreamAsync.Write(bytes2, 0, bytes2.Length);
            requestStreamAsync.Close();
            HttpWebResponse rsp = (HttpWebResponse)await req.GetResponseAsync();
            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return await GetResponseAsStringAsync(rsp, encoding);
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public HttpWebRequest GetWebRequest(string url, string method)
        {
            HttpWebRequest httpWebRequest;
            if (url.Contains("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.Method = method;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.UserAgent = "Yrq4Net";
            httpWebRequest.Timeout = _timeout;
            return httpWebRequest;
        }

        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader streamReader = null;
            try
            {
                stream = rsp.GetResponseStream();
                streamReader = new StreamReader(stream, encoding);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if (streamReader != null)
                    streamReader.Close();
                if (stream != null)
                    stream.Close();
                if (rsp != null)
                    rsp.Close();
            }
        }

        public async Task<string> GetResponseAsStringAsync(HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;
            string endAsync;
            try
            {
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                endAsync = await reader.ReadToEndAsync();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (stream != null)
                    stream.Close();
                if (rsp != null)
                    rsp.Close();
            }
            return endAsync;
        }

        public string BuildGetUrl(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
                url = !url.Contains("?") ? url + "?" + BuildQuery(parameters) : url + "&" + BuildQuery(parameters);
            return url;
        }

        public static string BuildQuery(IDictionary<string, string> parameters)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool flag = false;
            IEnumerator<KeyValuePair<string, string>> enumerator = parameters.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, string> current = enumerator.Current;
                string key = current.Key;
                current = enumerator.Current;
                string str = current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(str))
                {
                    if (flag)
                        stringBuilder.Append("&");
                    stringBuilder.Append(key);
                    stringBuilder.Append("=");
                    stringBuilder.Append(HttpUtility.UrlEncode(str, Encoding.UTF8));
                    flag = true;
                }
            }
            return stringBuilder.ToString();
        }
    }
}
