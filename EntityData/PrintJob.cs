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
        /// The ODFs of this Print Job
        /// </summary>
        public virtual IList<ODF> ODFs { get; set; } = new List<ODF>();
    }


    /// <summary>
    /// Status of the print job
    /// </summary>
    public enum PrintJobStatus
    { 
        READY,
        PRINTING,
        PRINTED,
        REMOVED
    }

}
