using System;
using System.Collections.Generic;
using System.Text;

namespace Web.DispenserClient
{
    /// <summary>
    /// Abstract response message
    /// </summary>
    public abstract class AResponseBase
    {
        /// <summary>
        /// Was the Response Successful
        /// </summary>
        public readonly bool IsSuccess;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isSuccess"></param>
        public AResponseBase(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    
    /// <summary>
    /// ODFTablet
    /// </summary>
    public sealed class ODFTablet
    { 
        /// <summary>
        /// Id of the ODF
        /// </summary>
        public Guid ODFId { get; set; }


        /// <summary>
        /// Label to be printed on the ODF
        /// </summary>
        public string Label { get; set; }  


        /// <summary>
        /// Dosage of the ODF
        /// </summary>
        public float Dosage { get; set; }
    }


    /// <summary>
    /// Initializes Print job Response
    /// </summary>
    public sealed class InitializePrintJobResponse : AResponseBase
    {
        /// <summary>
        /// Expoected time for completion of print
        /// </summary>
        public readonly DateTime ETA;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="eta"></param>
        /// <param name="isSuccess"></param>
        public InitializePrintJobResponse(DateTime eta, bool isSuccess) : base(isSuccess)
        {
            ETA = eta;
        }
    }


    /// <summary>
    /// Response to start print job
    /// </summary>
    public sealed class StartPrintJobResponse : AResponseBase
    {
        public StartPrintJobResponse(bool isSuccess) : base(isSuccess)
        {
        }
    }


    /// <summary>
    /// Respones for getting the status of the printer
    /// </summary>
    public sealed class GetPrinterStatusResponse : AResponseBase
    {
        public readonly PrinterStatus Status;


        public GetPrinterStatusResponse(PrinterStatus status, bool isSuccess) : base(isSuccess)
        {
            Status = status;
        }
    }


    /// <summary>
    /// Response for unlocking the dispenser
    /// </summary>
    public sealed class UnlockDispenserResponse : AResponseBase
    {
        public UnlockDispenserResponse(bool isSuccess) : base(isSuccess)
        {
        }
    }


    /// <summary>
    /// Status of the printer
    /// </summary>
    public enum PrinterStatus
    { 
        NO_CONNECTION = 0,
        IDLE = 1,
        PRINTING = 2
    }


}
