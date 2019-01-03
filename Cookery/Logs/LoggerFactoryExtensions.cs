using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Data;
using Microsoft.Extensions.Logging;

namespace Cookery.Web.Logs
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory,CookeryDbContext dbContext,Func<string, LogLevel, bool> filter = null)
        {
            factory.AddProvider(new DbLoggerProvider(filter, dbContext));
            return factory;
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel, CookeryDbContext dbContext)
        {
            return AddContext(
                factory,
                dbContext,
                (_, logLevel) => logLevel >= minLevel);
        }
    }
}
