using System;
using BookWarehouse.Core.Domain;

namespace BookWarehouse.Core.Service
{
    public interface ILogger
    {
        void Log(LogAction action, Guid titleId, string oldvalue, string newvalue);
    }
}
