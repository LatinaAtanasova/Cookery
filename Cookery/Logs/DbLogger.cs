using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Data;
using Cookery.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;

namespace Cookery.Web.Logs
{
    public class DbLogger : ILogger
    {
        private Func<string, LogLevel, bool> filter;
        private string categoryName;
        private CookeryDbContext dbContext;

        public DbLogger(Func<string, LogLevel, bool> filter,
            string categoryName, CookeryDbContext dbContext)
        {
            this.filter = filter;
            this.categoryName = categoryName;
            this.dbContext = dbContext;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.Error || exception != null)
            {
                try
                {
                    this.dbContext.CustomLogs.Add(new CustomLog
                    {
                        CreatedTime = DateTime.UtcNow,
                        EventId = 1,
                        LogLevel = logLevel.ToString(),
                        Message = $"{state}, {exception}"
                    });

                    dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    
                }


            }
        }
    }
}
