using System;
using System.Configuration;
using Castle.Core.Logging;
using NLog;
using NLog.Config;

namespace Rentrdoid.Common.Logger
{
    public class NLogFactory : AbstractLoggerFactory
    {
        private static bool _isFileWatched = false;

        public NLogFactory() :
            this(ConfigurationManager.AppSettings["Nlog.config"])
        {
        }
        public NLogFactory(string fileName)
        {
            if (!_isFileWatched && !string.IsNullOrWhiteSpace(fileName))
            {
                SimpleConfigurator.ConfigureForFileLogging(fileName);
                _isFileWatched = true;
            }
        }

        public override Castle.Core.Logging.ILogger Create(string name)
        {
            return new NLogger(LogManager.GetLogger(name), this);
        }

        public override Castle.Core.Logging.ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}