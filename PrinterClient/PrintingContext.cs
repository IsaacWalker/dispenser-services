using Google.Protobuf.Collections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.PrinterClient
{
    /// <summary>
    /// Context for the Printing 
    /// </summary>
    public sealed class PrintingContext : IPrintingContext
    {
        /// <summary>
        /// Store of all active prints
        /// </summary>
        private readonly IDictionary<Guid, PrintJob> _activePrintingJobs = new ConcurrentDictionary<Guid, PrintJob>();


        /// <summary>
        /// Store of prints that have been completed and removed
        /// </summary>
        private readonly IReadOnlyCollection<PrintJob> _completedPrintingJobs = new ConcurrentBag<PrintJob>();


        /// <summary>
        /// Printer client
        /// </summary>
        private readonly Printer.PrinterClient m_client;


        /// <summary>
        /// Active Printer jobs
        /// </summary>
        public IList<PrintJob> ActiveJobs => _activePrintingJobs.Values.ToList();


        /// <summary>
        /// Completed printer jobs
        /// </summary>
        public IList<PrintJob> CompletedJobs => _completedPrintingJobs.ToList();


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        public PrintingContext(Printer.PrinterClient client)
        {
            m_client = client;
        }


        /// <summary>
        /// Gets the status of a job
        /// </summary>
        /// <param name="printJob"></param>
        /// <returns></returns>
        public async Task<GetJobStatusResponse.Types.JobStatus> GetJobStatus(PrintJob printJob)
        {
            var request = new GetJobStatusRequest()
            {
                JobId = printJob.Id.ToString()
            };

            var jobStatus = await m_client.GetJobStatusAsync(request);
            return jobStatus.Status;
        }


        /// <summary>
        /// Creates a print job from an ODF
        /// </summary>
        /// <param name="odf"></param>
        /// <returns></returns>
        public async Task<PrintJob> CreatePrintJob(IList<ODF> odfs)
        {
            var request = new CreatePrintJobRequest();
            request.Odfs.AddRange(odfs);

            var response = await m_client.CreatePrintJobAsync(request);
            DateTime ETA = DateTime.Now + TimeSpan.FromSeconds(response.ExpectedDuration);

            PrintJob printJob = new PrintJob(Guid.Parse(response.JobId), ETA);
            _activePrintingJobs.Add(printJob.Id, printJob);

            return printJob;
        }


        /// <summary>
        /// Starts the specified print job
        /// </summary>
        /// <param name="printJob"></param>
        /// <returns></returns>
        public async Task<bool> StartPrintJob(PrintJob printJob)
        {
            RunPrintJobRequest request = new RunPrintJobRequest()
            {
                JobId = printJob.Id.ToString()
            };

            var response = await m_client.RunPrintJobAsync(request);
            return response.Success;
        }


        /// <summary>
        /// Gets the status of the printer
        /// </summary>
        /// <returns></returns>
        public async Task<GetPrinterStatusResponse.Types.PrinterStatus> GetPrinterStatus()
        {
            var request = new GetPrinterStatusRequest();
            var response = await m_client.GetPrinterStatusAsync(request);

            return response.Status;
        }
    }
}
