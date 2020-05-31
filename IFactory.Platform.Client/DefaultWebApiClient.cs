using IFactory.Common;
using IFactory.Platform.Common;
using IFactory.Platform.Common.Parser;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IFactory.Platform.Client
{
    public class DefaultWebApiClient : IWebApiClient
    {
        private string serverUrl;

        private string appKey;

        private string appSecret;

        private string accessToken;

        private string format = "json";

        private WebUtils webUtils;

        private IWebApiLogger topLogger;

        private bool disableParser;

        private bool disableTrace = true;

        public DefaultWebApiClient(string serverUrl, string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverUrl = serverUrl;
            this.webUtils = new WebUtils();
            this.topLogger = new DefaultWebApiLogger();
        }

        public DefaultWebApiClient(string serverUrl, string appKey, string appSecret, string sessionkey) : this(serverUrl, appKey, appSecret)
        {
            this.accessToken = sessionkey;
        }

        public void SetTimeout(int timeout)
        {
            this.webUtils.Timeout = timeout;
        }

        public void SetDisableParser(bool disableParser)
        {
            this.disableParser = disableParser;
        }

        public void SetDisableTrace(bool disableTrace)
        {
            this.disableTrace = disableTrace;
        }

        public void SetLogger(IWebApiLogger topLogger)
        {
            this.topLogger = topLogger;
        }

        public void SetAccessToken(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public T Execute<T>(IRequest<T> request) where T : BaseResponse
        {
            return this.Execute<T>(request, this.accessToken);
        }

        public T Execute<T>(IRequest<T> request, string accessToken) where T : BaseResponse
        {
            return this.Execute<T>(request, accessToken, DateTime.Now);
        }

        private T Execute<T>(IRequest<T> request, string accessToken, DateTime timestamp) where T : BaseResponse
        {
            if (this.disableTrace)
            {
                return this.DoExecute<T>(request, accessToken, timestamp);
            }
            T result;
            try
            {
                result = this.DoExecute<T>(request, accessToken, timestamp);
            }
            catch (Exception ex)
            {
                this.topLogger.Error(this.serverUrl + "\r\n" + ex.StackTrace);
                throw ex;
            }
            return result;
        }

        public async Task<T> ExecuteAsync<T>(IRequest<T> request) where T : BaseResponse
        {
            return await this.ExecuteAsync<T>(request, this.accessToken);
        }

        public async Task<T> ExecuteAsync<T>(IRequest<T> request, string accessToken) where T : BaseResponse
        {
            return await this.ExecuteAsync<T>(request, accessToken, DateTime.Now);
        }

        private async Task<T> ExecuteAsync<T>(IRequest<T> request, string accessToken, DateTime timestamp) where T : BaseResponse
        {
            T result;
            if (this.disableTrace)
            {
                result = await this.DoExecuteAsync<T>(request, accessToken, timestamp);
            }
            else
            {
                try
                {
                    result = await this.DoExecuteAsync<T>(request, accessToken, timestamp);
                }
                catch (Exception var_3_123)
                {
                    this.topLogger.Error(this.serverUrl + "\r\n" + var_3_123.StackTrace);
                    throw var_3_123;
                }
            }
            return result;
        }

        public string GetParamJson(IDictionary<string, string> parameters)
        {
            IEnumerator<KeyValuePair<string, string>> enumerator = ((IEnumerable<KeyValuePair<string, string>>)new SortedDictionary<string, string>(parameters)).GetEnumerator();
            StringBuilder stringBuilder = new StringBuilder("{");
            bool flag = true;
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, string> current = enumerator.Current;
                string key = current.Key;
                current = enumerator.Current;
                string value = current.Value;
                if (!string.IsNullOrEmpty(key))
                {
                    Trace.WriteLine(string.Format("参数：{0}  值：{1}", key, value));
                    if (!flag)
                    {
                        stringBuilder.Append(",");
                    }
                    stringBuilder.AppendFormat("\"{0}\":\"{1}\"", key, value);
                    flag = false;
                }
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        private T DoExecute<T>(IRequest<T> request, string accessToken, DateTime timestamp) where T : BaseResponse
        {
            T result;
            try
            {
                request.Validate();
            }
            catch (WebApiException ex)
            {
                result = this.createErrorResponse<T>(ex.ErrorCode, ex.ErrorMsg);
                return result;
            }
            IWebApiDictionary webApiDictionary = new IWebApiDictionary();
            webApiDictionary.Add("param_json", request.GetParamJson());
            webApiDictionary.Add("method", request.ApiName);
            webApiDictionary.Add("v", "1.0");
            webApiDictionary.Add("app_key", this.appKey);
            webApiDictionary.Add("timestamp", timestamp.ToUnixTime());
            webApiDictionary.Add("access_token", accessToken);
            webApiDictionary.Add("sign", WebApiUtils.SignYrqRequest(webApiDictionary, this.appSecret));
            string value = this.webUtils.BuildGetUrl(this.serverUrl, webApiDictionary);
            try
            {
                string text;
                if (request is IUploadRequest<T>)
                {
                    IDictionary<string, FileItem> fileParams = WebApiUtils.CleanupDictionary<FileItem>(((IUploadRequest<T>)request).GetFileParameters());
                    text = this.webUtils.DoPost(this.serverUrl, webApiDictionary, fileParams);
                }
                else
                {
                    text = this.webUtils.DoPost(this.serverUrl, webApiDictionary);
                }
                T t;
                if (this.disableParser)
                {
                    t = Activator.CreateInstance<T>();
                    t.Body = text;
                }
                else if ("json".Equals(this.format))
                {
                    IParser<T> parser = new JsonParser<T>();
                    if (request is ICustomRequest<T>)
                    {
                        t = ((ICustomRequest<T>)request).PareseResponse(text);
                    }
                    else
                    {
                        t = parser.Parse(text);
                    }
                }
                else
                {
                    t = new XmlParser<T>().Parse(text);
                }
                result = t;
            }
            catch (Exception ex2)
            {
                if (!this.disableTrace)
                {
                    StringBuilder stringBuilder = new StringBuilder(value).Append(" request error!\r\n").Append(ex2.StackTrace);
                    this.topLogger.Error(stringBuilder.ToString());
                }
                throw ex2;
            }
            return result;
        }

        private async Task<T> DoExecuteAsync<T>(IRequest<T> request, string accessToken, DateTime timestamp) where T : BaseResponse
        {
            T result;
            try
            {
                request.Validate();
            }
            catch (WebApiException ex)
            {
                result = this.createErrorResponse<T>(ex.ErrorCode, ex.ErrorMsg);
                return result;
            }
            IWebApiDictionary webApiDictionary = new IWebApiDictionary();
            webApiDictionary.Add("param_json", request.GetParamJson());
            webApiDictionary.Add("method", request.ApiName);
            webApiDictionary.Add("v", "1.0");
            webApiDictionary.Add("app_key", this.appKey);
            webApiDictionary.Add("timestamp", timestamp.ToUnixTime());
            webApiDictionary.Add("access_token", accessToken);
            webApiDictionary.Add("sign", WebApiUtils.SignYrqRequest(webApiDictionary, this.appSecret));
            string value = this.webUtils.BuildGetUrl(this.serverUrl, webApiDictionary);
            try
            {
                string text;
                if (request is IUploadRequest<T>)
                {
                    IDictionary<string, FileItem> fileParams = WebApiUtils.CleanupDictionary(((IUploadRequest<T>)request).GetFileParameters());
                    text = await this.webUtils.DoPostAsync(this.serverUrl, webApiDictionary, fileParams);
                }
                else
                {
                    text = await this.webUtils.DoPostAsync(this.serverUrl, webApiDictionary);
                }
                T t;
                if (this.disableParser)
                {
                    t = Activator.CreateInstance<T>();
                    t.Body = text;
                }
                else if ("json".Equals(this.format))
                {
                    IParser<T> var_7_2B3 = new JsonParser<T>();
                    if (request is ICustomRequest<T>)
                    {
                        t = ((ICustomRequest<T>)request).PareseResponse(text);
                    }
                    else
                    {
                        t = var_7_2B3.Parse(text);
                    }
                }
                else
                {
                    t = new XmlParser<T>().Parse(text);
                }
                result = t;
            }
            catch (Exception var_8_2F5)
            {
                if (!this.disableTrace)
                {
                    object var_9_325 = new StringBuilder(value).Append(" request error!\r\n").Append(var_8_2F5.StackTrace);
                    this.topLogger.Error(var_9_325.ToString());
                }
                throw var_8_2F5;
            }
            return result;
        }

        private T createErrorResponse<T>(string errCode, string errMsg) where T : BaseResponse
        {
            T t = Activator.CreateInstance<T>();
            if ("json".Equals(this.format))
            {
                IDictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("code", errCode);
                dictionary.Add("msg", errMsg);
                string body = JsonConvert.SerializeObject(new Dictionary<string, object>
                {
                    {
                        "error_response",
                        dictionary
                    }
                });
                t.Body = body;
            }
            else
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlElement xmlElement = xmlDocument.CreateElement("error_response");
                XmlElement xmlElement2 = xmlDocument.CreateElement("code");
                xmlElement2.InnerText = errCode;
                xmlElement.AppendChild(xmlElement2);
                XmlElement xmlElement3 = xmlDocument.CreateElement("msg");
                xmlElement3.InnerText = errMsg;
                xmlElement.AppendChild(xmlElement3);
                xmlDocument.AppendChild(xmlElement);
                t.Body = xmlDocument.OuterXml;
            }
            return t;
        }
    }
}
