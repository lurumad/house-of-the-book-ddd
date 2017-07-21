using System;

namespace Microsoft.Extensions.Logging
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddIf(
            this ILoggerFactory loggerFactory,
            bool condition,
            Func<ILoggerFactory,ILoggerFactory> action)
        {
            return condition ? action(loggerFactory) : loggerFactory;
        }

        public static ILoggerFactory AddIfElse(
            this ILoggerFactory loggerFactory,
            bool condition,
            Func<ILoggerFactory, ILoggerFactory> ifAction,
            Func<ILoggerFactory, ILoggerFactory> elseAction)
        {
            return condition ? ifAction(loggerFactory) : elseAction(loggerFactory);
        }
    }
}
