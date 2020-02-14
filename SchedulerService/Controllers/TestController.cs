using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.PrinterClient;

namespace Web.SchedulerService.Controllers
{
    /// STUB - used for testing gRPC cpommunication
    
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IPrinterClient m_client;


        public TestController(IPrinterClient client)
        {
            m_client = client;
        }


       [HttpGet()]
        public Task<string> Get()
        {
            /*  PrintMedicationRequest request = new PrintMedicationRequest()
              {
                  FirstName = "Chad",
                  LastName = "Chadson",
                  Dosage = 12f,
                  DrugName = "Eatitol"
              };

              var response = await m_client.PrintMedicationAsync(request);

              return "Duration: " + response.ExpectedDuration;*/
            return Task.FromResult("Result");
        }
    }
}
