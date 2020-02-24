using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;

namespace Web.SchedulerService.Scheduler
{
    /// <summary>
    /// Represents a schedule for a day, tracking of the print jobs and their state
    /// </summary>
    internal interface IDailyPrintSchedule
    {
        DateTime Date { get; }

        // TODO 
    }
}
