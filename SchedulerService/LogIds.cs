using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SchedulerService
{
    public static class LogIds
    {
        public static class Information
        {
            public static readonly EventId StartPrinterHealthCheck = 0;


            public static readonly EventId EndtPrinterHealthCheck = 1;


            public static readonly EventId StartSchedulerHealthCheck = 2;


            public static readonly EventId EndSchedulerHealthCheck = 3;
        }

    }
}
