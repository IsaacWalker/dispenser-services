using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            return Ok(model);
        }
    }
}