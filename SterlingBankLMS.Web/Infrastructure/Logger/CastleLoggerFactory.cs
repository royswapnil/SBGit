using Rentrdoid.Common.Logger;

namespace Orchard.Logging
{
    public class CastleLoggerFactory : ILoggerFactory {
        private readonly Castle.Core.Logging.ILoggerFactory _castleLoggerFactory;

        public CastleLoggerFactory(Castle.Core.Logging.ILoggerFactory castleLoggerFactory) {
            _castleLoggerFactory = castleLoggerFactory;
        }

        public ILogger CreateLogger(string type) {
            return new CastleLogger(_castleLoggerFactory.Create(type));
        }
    }
}