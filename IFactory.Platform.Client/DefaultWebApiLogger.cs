using System;
using System.Diagnostics;

namespace IFactory.Platform.Client
{
    public class DefaultWebApiLogger : IWebApiLogger
    {
        public const string LOG_FILE_NAME = "topsdk.log";
        public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        static DefaultWebApiLogger()
        {
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener("topsdk.log"));
            }
            catch (Exception ex)
            {
                Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            }
            Trace.AutoFlush = true;
        }

        public void Error(string message)
        {
            Trace.WriteLine(message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ERROR");
        }

        public void Warn(string message)
        {
            Trace.WriteLine(message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " WARN");
        }

        public void Info(string message)
        {
            Trace.WriteLine(message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " INFO");
        }
    }
}
