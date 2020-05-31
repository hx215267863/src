using System;

namespace IFactory.Common.Logs
{
    public abstract class LoggerBase : ILogger
    {
        private ILogger m_NestedLogger;

        public string Name { get; private set; }

        private LoggerBase()
        {
        }

        public LoggerBase(string name)
          : this(name, null)
        {
        }

        public LoggerBase(string name, ILogger nestedLogger)
        {
            Name = name;
            m_NestedLogger = nestedLogger;
        }

        public virtual void LogError(Exception e)
        {
            if (m_NestedLogger == null)
                return;
            m_NestedLogger.LogError(e);
        }

        public virtual void LogError(string title, Exception e)
        {
            if (m_NestedLogger == null)
                return;
            m_NestedLogger.LogError(title, e);
        }

        public virtual void LogError(string message)
        {
            if (m_NestedLogger == null)
                return;
            m_NestedLogger.LogError(message);
        }

        public virtual void LogDebug(string message)
        {
            if (m_NestedLogger == null)
                return;
            m_NestedLogger.LogDebug(message);
        }

        public virtual void LogInfo(string message)
        {
            if (m_NestedLogger == null)
                return;
            m_NestedLogger.LogInfo(message);
        }

        public virtual void LogPerf(string message)
        {
            if (m_NestedLogger == null)
                return;
            m_NestedLogger.LogPerf(message);
        }
    }
}
