using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SchedulerService.Scheduler
{
    internal sealed class DailyPrintSchedule : IDailyPrintSchedule
    {
        public DateTime Date { get; private set; }


        public DailyPrintSchedule(DateTime date)
        {
            Date = date;
        }
    }
}
