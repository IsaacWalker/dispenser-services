using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.PrinterClient;

namespace Web.SchedulerService.Scheduler
{
    /// <summary>
    /// A print job
    /// </summary>
    internal sealed class PrintJob
    {
        /// <summary>
        /// Id of the Job
        /// </summary>
        public readonly string Id;


        /// <summary>
        /// ETA of the finish time
        /// </summary>
        public readonly DateTime ETA;


        /// <summary>
        /// The ODF that's used for the job
        /// </summary>
        public readonly ODF ODF;


        public PrintJob(string Id, DateTime ETA, ODF ODF)
        {
            this.Id = Id;
            this.ETA = ETA;
            this.ODF = ODF;
        }
    }
}
