using System;
using System.Runtime.Serialization;

namespace IFactory.Platform.Common
{
    public class WebApiException : Exception
    {
        private string errorCode;
        private string errorMsg;

        public string ErrorCode
        {
            get
            {
                return errorCode;
            }
        }

        public string ErrorMsg
        {
            get
            {
                return errorMsg;
            }
        }

        public WebApiException()
        {
        }

        public WebApiException(string message)
          : base(message)
        {
        }

        protected WebApiException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }

        public WebApiException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        public WebApiException(string errorCode, string errorMsg)
          : base(errorCode + ":" + errorMsg)
        {
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
        }
    }
}
