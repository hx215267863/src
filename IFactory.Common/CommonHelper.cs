using System;

namespace IFactory.Common
{
    public class CommonHelper
    {
        public static string GetCraftShortNO(string craftNO)
        {
            return craftNO.Split('_')[1];
        }

        public static string GetProcessShortNO(string processNO)
        {
            return processNO.Split('_')[2];
        }

        public static string GetNextVerificationCode(int length)
        {
            Random random = new Random();
            string str = string.Empty;
            for (int index = 0; index < length; ++index)
                str += random.Next(10).ToString();
            return str;
        }
    }
}
