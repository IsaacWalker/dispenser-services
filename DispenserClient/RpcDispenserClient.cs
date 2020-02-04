

using Grpc.Net.Client;
using System.Threading.Tasks;

namespace Web.DispenserClient
{
    public class RpcDispenserClient : IDispenserClient
    {
        private static readonly string _address = "";


        public async Task<PrintMedicationResponse> PrintMedication(PrintMedicationRequest request)
        {
            using (var channel = GrpcChannel.ForAddress(_address))
            {
                var client = new Dispenser.DispenserClient(channel);
                var response = await client.PrintMedicationAsync(request);
                return response;
            }
        }
    }
}
