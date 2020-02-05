using System;
using System.Threading.Tasks;

namespace Web.DispenserClient
{
    /// <summary>
    /// Interface for communication with the dispenser/printer
    /// </summary>
    public interface IDispenserClient
    {
        /// <summary>
        /// Starts printing the medication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PrintMedicationResponse> PrintMedicationAsync(PrintMedicationRequest request);
    }

}
