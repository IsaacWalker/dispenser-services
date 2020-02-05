

using Grpc.Net.Client;
using System.Threading.Tasks;

namespace Web.DispenserClient
{
    /// <summary>
    /// gRPC client for communication with dispenser/printer
    /// </summary>
    public class RpcDispenserClient : IDispenserClient
    {
        private readonly Dispenser.DispenserClient _client;


        public RpcDispenserClient(Dispenser.DispenserClient client)
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
