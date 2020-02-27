using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Web.PrinterClient
{
    public interface IPrintingContext
    {
        /// <summary>
        /// Gets the active printing jobs
        /// </summary>
        public IList<PrintJob> ActiveJobs { get; }


        /// <summary>
        /// Gets the completed printing jobs
        /// </summary>
        public IList<PrintJob> CompletedJobs { get; }


        /// <summary>
        /// Gets the status of a job
        /// </summary>
        /// <param name="printJob"></param>
        /// <returns></returns>
        public Task<GetJobStatusResponse.Types.JobStatus> GetJobStatus(PrintJob printJob);


        /// <summary>
        /// Creates a print job from an ODF
        /// </summary>
        /// <param name="odf"></param>
        /// <returns></returns>
        public Task<PrintJob> CreatePrintJob(ODF odf);


        /// <summary>
        /// Starts a printing job
        /// </summary>
        /// <param name="printJob"></param>
        /// <returns></returns>
        public Task<bool> StartPrintJob(PrintJob printJob);


        /// <summary>
        /// Gets the status of a printer
        /// </summary>
        /// <returns></returns>
        public Task<GetPrinterStatusResponse.Types.PrinterStatus> GetPrinterStatus();
    }
}
