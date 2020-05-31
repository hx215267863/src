namespace IFactory.Platform.Common.Util
{
    public sealed class RequestValidator
    {
        private const string ERR_CODE_PARAM_MISSING = "40";
        private const string ERR_CODE_PARAM_INVALID = "41";
        private const string ERR_MSG_PARAM_MISSING = "client-error:Missing required arguments:{0}";
        private const string ERR_MSG_PARAM_INVALID = "client-error:Invalid arguments:{0}";

        public static void ValidateRequired(string name, object value)
        {
            if (value == null)
                throw new WebApiException("40", string.Format("client-error:Missing required arguments:{0}", name));
            if (value.GetType() == typeof(string) && string.IsNullOrEmpty(value as string))
                throw new WebApiException("40", string.Format("client-error:Missing required arguments:{0}", name));
        }

        public static void ValidateMaxLength(string name, string value, int maxLength)
        {
            if (value != null && value.Length > maxLength)
                throw new WebApiException("41", string.Format("client-error:Invalid arguments:{0}", name));
        }

        public static void ValidateMaxLength(string name, FileItem value, int maxLength)
        {
            if (value != null && value.GetContent() != null && value.GetContent().Length > maxLength)
                throw new WebApiException("41", string.Format("client-error:Invalid arguments:{0}", name));
        }

        public static void ValidateMaxListSize(string name, string value, int maxSize)
        {
            if (value == null)
                return;
            string[] strArray = value.Split(',');
            if (strArray != null && strArray.Length > maxSize)
                throw new WebApiException("41", string.Format("client-error:Invalid arguments:{0}", name));
        }

        public static void ValidateMinLength(string name, string value, int minLength)
        {
            if (value != null && value.Length < minLength)
                throw new WebApiException("41", string.Format("client-error:Invalid arguments:{0}", name));
        }

        public static void ValidateMaxValue(string name, long? value, long maxValue)
        {
            if (!value.HasValue)
                return;
            long? nullable = value;

            long num = maxValue;
            if ((nullable.GetValueOrDefault() > num ? (nullable.HasValue ? 1 : 0) : 0) != 0)
                throw new WebApiException("41", string.Format("client-error:Invalid arguments:{0}", name));
        }

        public static void ValidateMinValue(string name, long? value, long minValue)
        {
            if (!value.HasValue)
                return;
            long? nullable = value;
            long num = minValue;
            if ((nullable.GetValueOrDefault() < num ? (nullable.HasValue ? 1 : 0) : 0) != 0)
                throw new WebApiException("41", string.Format("client-error:Invalid arguments:{0}", name));
        }
    }
}
