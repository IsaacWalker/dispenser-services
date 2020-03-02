using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;
using Web.SchedulerService.Models.PageModels;

namespace Web.SchedulerService.Controllers
{
    [ApiController]
    public class HomeController : AMobileControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public HomeController(IServiceProvider serviceProvider, ILogger<HomeController> logger) : base(serviceProvider, logger)
        {
        }


        /// <summary>
        /// Gets the model for the Home of the app
        /// </summary>
        /// <param name="nurseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get([FromQuery] Guid nurseId)
        {
            m_logger.LogDebug("Getting Home view for Nurse {0}", nurseId);

            HomePageModel model = new HomePageModel();

            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                Nurse nurse = context.Nurses.Find(nurseId);

                if(nurse == default)
                {
                    m_logger.LogWarning("No nurse found for id {0}", nurseId);

                    return NotFound();
                }

                model.NurseName = nurse.FirstName;

                var currentJob = context.PrintJobs
                    .Where(Job => Job.Status == PrintJobStatus.PRINTING || Job.Status == PrintJobStatus.PRINTED)
                    .FirstOrDefault();

                if (currentJob != null)
                {
                    model.ODFs = currentJob.ODFs
                     .Select(O => new BatchODF()
                     {
                         PatientName = O.PrescriptionTime.Prescription.Patient.FirstName,
                         MedicationName = O.PrescriptionTime.Prescription.DrugName,
                         ODFId = O.Id,
                         PatientId = O.PrescriptionTime.Prescription.Patient.Id
                     })
                    .ToList();

                    model.PrintJobId = currentJob.Id;

                    m_logger.LogDebug("Returning Home Model for Nurse {0} with {1} ODF's", nurseId, model.ODFs.Count);
                }
                else;
                {
                    model.ODFs = new List<BatchODF>();
                }

            }

           
            return Ok(model);
            
        }
    }
}
