using Rentrdoid.Common.Logger;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace WebLogic.Infrastructure.Logger
{
    public class NLogExceptionHandler : IExceptionLogger
    {
        private readonly ILogger _logger;
        public NLogExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }

        Task IExceptionLogger.LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            //if (_logger.IsEnabled(LogLevel.Error))
                return Task.Run(() =>
                    _logger.Error(context.Exception, context.Exception.Message)
                ,cancellationToken);

            //return Task.FromResult(0);
        }
    }
}