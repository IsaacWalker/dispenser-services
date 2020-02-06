

using Grpc.Net.Client;
using System.Threading.Tasks;

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


        public async Task<PrintMedicationResponse> PrintMedicationAsync(PrintMedicationRequest request)
        {
             var response = await _client.PrintMedicationAsync(request);
             return response;
            
        }
    }
}
