using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// A print job, which consists of a batch of ODF's
    /// </summary>
    public class PrintJob : AEntityBase
    {
        /// <summary>
        /// Status of the Print Job
        /// </summary>
        public PrintJobStatus Status { get; set; }


        /// <summary>
        /// Time it is expected to be in the READY state at
        /// </summary>
        public DateTime ExpectedTimeOfReadiness { get; set; }


        /// <summary>
        /// The ODFs of this Print Job
        /// </summary>
        public virtual IList<ODF> ODFs { get; set; } = new List<ODF>();


        /// <summary>
        /// Daily Schedule Id
        /// </summary>
        public Guid DailyScheduleId { get; set; }


        /// <summary>
        /// The schedule this is associated with
        /// </summary>
        public virtual DailySchedule DailySchedule { get; set; }
    }


    /// <summary>
    /// Status of the print job
    /// </summary>
    public enum PrintJobStatus
    { 
        UNINITIALIZED,
        READY,
        PRINTING,
        PRINTED,
        REMOVED
    }

}
