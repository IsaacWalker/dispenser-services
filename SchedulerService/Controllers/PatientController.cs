using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.EntityData;
using Web.SchedulerService.Models;

namespace Web.SchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ServiceControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<PatientController> m_logger;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public PatientController(IServiceProvider serviceProvider, ILogger<PatientController> logger)
            : base(serviceProvider)
        {
            m_logger = logger;
        }


        /// <summary>
        /// Gets patient Info with a Particular Id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int patientId)
        {
            m_logger.LogInformation("Starting request for GET Patient with Id {0}", patientId);


            using (var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                var patient = await context.Patients.FindAsync(patientId);


                if (patient == default)
                {
                    m_logger.LogWarning("No Patient found with Id {0}", patientId);

                    return NotFound();
                }

                var patientModel = new PatientModel()
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DateOfBirth = patient.DateOfBirth
                };

                m_logger.LogInformation("Patient found with Id {0}", patientId);

                return Ok(patientModel);
            }
        }
    }
}