using System;
using BookWarehouse.Core.Domain;
using BookWarehouse.Core.Infrastructure;
using BookWarehouse.Core.Service;

namespace BookWarehouse.Service
{
    public class DbLogger : ILogger
    {
        private readonly IRepository<Log> _log;

        public DbLogger(IRepository<Log> log)
        {
            _log = log;
        }

        public void Log(LogAction action, Guid titleId, string oldvalue, string newvalue)
        {
            _log.Add(new Log
            {
                Action = Enum.GetName(typeof(LogAction), action),
                Timestamp = DateTime.UtcNow,
                OldValue = oldvalue,
                NewValue = newvalue,
                TitleId = titleId
            });
        }
    }
}
