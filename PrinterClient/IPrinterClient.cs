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
        /// Starts printing the medication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PrintMedicationResponse> PrintMedicationAsync(PrintMedicationRequest request);


        /// <summary>
        /// Checks the health of the printer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckPrinterHealthResponse> CheckPrinterHealthAsync(CheckPrinterHealthRequest request);
    }

}
