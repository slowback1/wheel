using Common.Data;
using System;
using System.Threading.Tasks;

namespace Common.Interfaces;

public interface IIntervalUpdater
{
    Task<SaveResult<Interval>> UpdateIntervalLastRun(string username, string intervalName, DateTime lastRunTime, DateTime nextRunTime);
}
