using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Data;
using Microsoft.Extensions.Logging;

namespace Cookery.Web.Logs
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private Func<string, LogLevel, bool> filter;
        private CookeryDbContext dbContext;

        public DbLoggerProvider(Func<string, LogLevel, bool> filter,
            CookeryDbContext dbContext)
        {
            this.filter = filter;
            this.dbContext = dbContext;
        }


        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(filter, categoryName, dbContext);
        }

        public void Dispose()
        {
            
        }
    }
}
