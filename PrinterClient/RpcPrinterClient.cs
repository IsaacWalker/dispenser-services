

using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using static Web.PrinterClient.CheckPrinterHealthResponse.Types;

namespace Web.PrinterClient
{
    /// <summary>
    /// gRPC client for communication with dispenser/printer
    /// </summary>
    public class RpcPrinterClient : IPrinterClient
    {
        private readonly Printer.PrinterClient _client;


        public RpcPrinterClient(Printer.PrinterClient client)
        {
            _client = client;
        }

        public async Task<CheckPrinterHealthResponse> CheckPrinterHealthAsync(CheckPrinterHealthRequest request)
        {
            try
            {
                var response = await _client.CheckHealthAsync(request);
                return response;
            }
            catch(Exception _)
            {
                return new CheckPrinterHealthResponse() 
                { 
                    Status = HealthCheckStatus.NoConnection 
                };
            }
            
        }

        public async Task<PrintMedicationResponse> PrintMedicationAsync(PrintMedicationRequest request)
        {
            try
            {
                var response = await _client.PrintMedicationAsync(request);
                
                return response;
            }
            catch(Exception _)
            {
                return null;
            }
            
        }
    }
}
