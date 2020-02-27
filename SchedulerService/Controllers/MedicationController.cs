using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.SchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ServiceControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        public readonly ILogger<MedicationController> m_logger;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceProvider"></param>
        public MedicationController(ILogger<MedicationController> logger, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            m_logger = logger;
        }


        /// <summary>
        /// Gets the Medication
        /// </summary>
        /// <returns></returns>
        public IActionResult Get()
        {
            return Ok();
        }
    }
}