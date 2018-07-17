using System;
using ICastleLogger = Castle.Core.Logging.ILogger;

namespace Rentrdoid.Common.Logger
{
    public class NLogger : MarshalByRefObject, ICastleLogger
    {
        private static readonly Type declaringType = typeof(NLogger);

        public NLogger(NLog.ILogger logger, NLogFactory factory)
        {
            Logger = logger;
            Factory = factory;
        }

        protected internal NLogFactory Factory { get; private set; }
        protected internal NLog.ILogger Logger { get; private set; }

        public bool IsDebugEnabled
        {
            get { return Logger.IsEnabled(NLog.LogLevel.Debug); }
        }

        public bool IsErrorEnabled
        {
            get { return Logger.IsEnabled(NLog.LogLevel.Error); }
        }

        public bool IsFatalEnabled
        {
            get { return Logger.IsEnabled(NLog.LogLevel.Fatal); }
        }

        public bool IsInfoEnabled
        {
            get { return Logger.IsEnabled(NLog.LogLevel.Info); }
        }

        public bool IsWarnEnabled
        {
            get { return Logger.IsEnabled(NLog.LogLevel.Warn); }
        }

        public override string ToString()
        {
            return Logger.ToString();
        }

        public ICastleLogger CreateChildLogger(string loggerName)
        {
            return Factory.Create(Logger.Name + "." + loggerName);

        }

        public void Debug(Func<string> messageFactory)
        {
            if (IsDebugEnabled)
                Logger.Log(NLog.LogLevel.Debug, messageFactory.Invoke());
        }

        public void Debug(string message)
        {
            if (IsDebugEnabled)
                Logger.Log(NLog.LogLevel.Debug, message);
        }

        public void Debug(string message, Exception exception)
        {
            if (IsDebugEnabled)
                Logger.Log(NLog.LogLevel.Debug, exception, message);
        }

        public void DebugFormat(string format, params object[] args)
        {
            if (IsDebugEnabled)
                Logger.Log(NLog.LogLevel.Debug, format, args);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsDebugEnabled)
                Logger.Log(NLog.LogLevel.Debug, formatProvider, format, args);
        }

        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            if (IsDebugEnabled)
                Logger.Log(NLog.LogLevel.Debug, exception, format, args);
        }

        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsDebugEnabled)
                Logger.Log(NLog.LogLevel.Debug, exception, formatProvider, format, args);
        }

        public void Error(Func<string> messageFactory)
        {
            if (IsErrorEnabled)
                Logger.Log(NLog.LogLevel.Error, messageFactory.Invoke());
        }

        public void Error(string message)
        {
            if (IsErrorEnabled)
                Logger.Log(NLog.LogLevel.Error, message);
        }

        public void Error(string message, Exception exception)
        {
            if (IsErrorEnabled)
                Logger.Log(NLog.LogLevel.Error, exception, message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            if (IsErrorEnabled)
                Logger.Log(NLog.LogLevel.Error, format, args);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsErrorEnabled)
                Logger.Log(NLog.LogLevel.Error, formatProvider, format, args);
        }

        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
            if (IsErrorEnabled)
                Logger.Log(NLog.LogLevel.Error, exception, format, args);
        }

        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsErrorEnabled)
                Logger.Log(NLog.LogLevel.Error, exception, formatProvider, format, args);
        }

        public void Fatal(Func<string> messageFactory)
        {
            if (IsFatalEnabled)
                Logger.Log(NLog.LogLevel.Fatal, messageFactory.Invoke());
        }

        public void Fatal(string message)
        {
            if (IsFatalEnabled)
                Logger.Log(NLog.LogLevel.Fatal, message);
        }

        public void Fatal(string message, Exception exception)
        {
            if (IsFatalEnabled)
                Logger.Log(NLog.LogLevel.Fatal, exception, message);
        }

        public void FatalFormat(string format, params object[] args)
        {
            if (IsFatalEnabled)
                Logger.Log(NLog.LogLevel.Fatal, format, args);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsFatalEnabled)
                Logger.Log(NLog.LogLevel.Fatal, formatProvider, format, args);
        }

        public void FatalFormat(Exception exception, string format, params object[] args)
        {
            if (IsFatalEnabled)
                Logger.Log(NLog.LogLevel.Fatal, exception, format, args);
        }

        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsFatalEnabled)
                Logger.Log(NLog.LogLevel.Fatal, exception, formatProvider, format, args);
        }

        public void Info(Func<string> messageFactory)
        {
            if (IsInfoEnabled)
                Logger.Log(NLog.LogLevel.Info, messageFactory.Invoke());
        }

        public void Info(string message)
        {
            if (IsInfoEnabled)
                Logger.Log(NLog.LogLevel.Info, message);
        }

        public void Info(string message, Exception exception)
        {
            if (IsInfoEnabled)
                Logger.Log(NLog.LogLevel.Info, exception, message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            if (IsInfoEnabled)
                Logger.Log(NLog.LogLevel.Info, format, args);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsInfoEnabled)
                Logger.Log(NLog.LogLevel.Info, formatProvider, format, args);
        }

        public void InfoFormat(Exception exception, string format, params object[] args)
        {
            if (IsInfoEnabled)
                Logger.Log(NLog.LogLevel.Info, exception, format, args);
        }

        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsInfoEnabled)
                Logger.Log(NLog.LogLevel.Info, exception, formatProvider, format, args);
        }

        public void Warn(Func<string> messageFactory)
        {
            if (IsWarnEnabled)
                Logger.Log(NLog.LogLevel.Warn, messageFactory.Invoke());
        }

        public void Warn(string message)
        {
            if (IsWarnEnabled)
                Logger.Log(NLog.LogLevel.Warn, message);
        }

        public void Warn(string message, Exception exception)
        {
            if (IsWarnEnabled)
                Logger.Log(NLog.LogLevel.Warn, exception, message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            if (IsWarnEnabled)
                Logger.Log(NLog.LogLevel.Warn, format, args);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsWarnEnabled)
                Logger.Log(NLog.LogLevel.Warn, formatProvider, format, args);
        }

        public void WarnFormat(Exception exception, string format, params object[] args)
        {
            if (IsWarnEnabled)
                Logger.Log(NLog.LogLevel.Warn, exception, format, args);
        }

        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsWarnEnabled)
                Logger.Log(NLog.LogLevel.Warn, exception, formatProvider, format, args);
        }
    }
}
