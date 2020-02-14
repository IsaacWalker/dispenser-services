using System;
using System.Threading.Tasks;

namespace Web.PrinterClient
{
    /// <summary>
    /// Interface for communication with the dispenser/printer
    /// </summary>
    public interface IPrinterClient
    {
        /// <summary>
        /// Creates a print job
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CreatePrintjobResponse> CreatePrintJobRequest(CreatePrintJobRequest request);
        

        /// <summary>
        /// Runs a print job
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RunPrintJobResponse> RunPrintJob(RunPrintJobRequest request);


        /// <summary>
        /// Gets the status of the job
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetJobStatusResponse> GetJobStatus(GetJobStatusRequest request);


        /// <summary>
        /// Checks the health of the printer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckPrinterHealthResponse> CheckPrinterHealthAsync(CheckPrinterHealthRequest request);
    }

}
