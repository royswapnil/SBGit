using System;

namespace Rentrdoid.Common.Logger
{
    public enum LogLevel
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }
    public interface ILogger
    {
        bool IsEnabled(LogLevel level);
        void Log(LogLevel level, Exception exception, string format, params object[] args);
    }

    public interface ILoggerFactory
    {
        ILogger CreateLogger(string type);
    }
}