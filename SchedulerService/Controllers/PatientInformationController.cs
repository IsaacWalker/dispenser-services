using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.EntityData;
using Web.SchedulerService.Models.PageModels;

namespace Web.SchedulerService.Controllers
{
    [ApiController]
    public class PatientInformationController : AMobileControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public PatientInformationController(IServiceProvider serviceProvider, ILogger<PatientInformationController> logger) 
            : base(serviceProvider, logger)
        {
        }


        /// <summary>
        /// Gets the model for the patient information screen
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get([FromQuery] Guid patientId)
        {
            PatientInformationPageModel model = new PatientInformationPageModel();

            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                Patient patient = context.Patients.Find(patientId);

                if(patientId == default)
                {
                    return NotFound(patientId);
                }

                model.FirstName = patient.FirstName;
                model.Surname = patient.LastName;

                /// Get all the ODF's for a patient for that day
                var prescriptionTimes = context.Prescriptions
                    .Where(P => P.PatientId == patientId)
                    .Include(P => P.Times)
                    .Select(P => P.Times);

             //   prescriptionTimes.Where(T => T.)
            }

            return Ok(model);
        }
    }
}