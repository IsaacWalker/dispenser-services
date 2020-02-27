using System;
using System.Collections.Generic;
using System.Text;

namespace Web.PrinterClient
{
    /// <summary>
    /// A Handle to a PrintJob
    /// </summary>
    public sealed class PrintJob
    {
        /// <summary>
        /// Id of the print job
        /// </summary>
        public readonly Guid Id;


        /// <summary>
        /// Expected Time that the drug will be printed on
        /// </summary>
        public readonly DateTime ETA;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eta"></param>
        public PrintJob(Guid id, DateTime eta)
        {
            Id = id;
            ETA = eta;
        }
    }
}
