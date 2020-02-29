using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.DispenserClient
{
    /// <summary>
    /// Dispenser Client
    /// </summary>
    public interface IDispenserClient
    {
        /// <summary>
        /// Initializes the print job
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<InitializePrintJobResponse> InitializePrintJobAsync(Guid JobId, IList<ODFTablet> ODFs);


        /// <summary>
        /// Starts the specified print job
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        public Task<StartPrintJobResponse> StartPrintJobAsync(Guid JobId);


        /// <summary>
        /// Gets the printer status
        /// </summary>
        /// <returns></returns>
        public Task<GetPrinterStatusResponse> GetPrinterStatusAsync();


        /// <summary>
        /// Unlocks the dispenser
        /// </summary>
        /// <returns></returns>
        public Task<UnlockDispenserResponse> UnlockDispenserAsync();
    }
}
