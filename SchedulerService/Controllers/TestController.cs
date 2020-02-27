using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.EntityData;
using Web.PrinterClient;
using Web.SchedulerService.Medication;

namespace Web.SchedulerService.Controllers
{
    /// STUB - used for testing gRPC cpommunication
    
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IPrintingContext m_context;


        private readonly IODFGenerator m_generator;


        public TestController(IPrintingContext context, IODFGenerator generator)
        {
            m_context = context;
            m_generator = generator;
        }


       [HttpGet()]
        public async Task<IActionResult> Get()
        {
            Patient patient = new Patient
            {
                FirstName = "T",
                LastName = "Davis",
                Height = 178.5f,
                Weight = 140.0f,
                Id = 0
            };

            Prescription prescription = new Prescription()
            {
                Id = 2,
                Dosage = 0.89f,
                Drug = "Adderall",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(7),
                Times = { DateTime.Now },
                Patient = patient,
                PatientId = patient.Id
            };

            ODF odf = m_generator.Run(prescription);
            var createjob_response = await m_context.CreatePrintJob(odf);

            /* string Id = createjob_response.JobId;

             RunPrintJobRequest request = new RunPrintJobRequest()
             {
                 JobId = Id
             };

             var runjob_response = await m_client.RunPrintJob(request);

             GetJobStatusRequest getjobStatus_request = new GetJobStatusRequest()
             { 
                 JobId = Id
             };

             var getjobstatus_response = await m_client.GetJobStatus(getjobStatus_request);*/

            return Ok(createjob_response.Id);// + " " + getjobstatus_response.Status);
        }
    }
}
