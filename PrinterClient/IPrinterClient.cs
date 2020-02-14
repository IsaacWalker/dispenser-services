using System;
using System.Threading.Tasks;

namespace Web.PrinterClient
{
    /// <summary>
    /// Interface for communication with the dispenser/printer
    /// </summary>
    public interface IPrinterClient
    {
        Task<CreatePrintjobResponse> CreatePrintJobRequest(CreatePrintJobRequest request);

        Task<RunPrintJobResponse> RunPrintJob(RunPrintJobRequest request);


        /// <summary>
        /// Checks the health of the printer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckPrinterHealthResponse> CheckPrinterHealthAsync(CheckPrinterHealthRequest request);
    }

}
