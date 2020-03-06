using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.EntityData;

namespace Web.SchedulerService.Controllers
{
    [ApiController]
    public class SchedulerController : APIControllerBase
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public SchedulerController(IServiceProvider serviceProvider, ILogger logger) : base(serviceProvider, logger)
        {
        }


        /// <summary>
        /// Sets a printJob to finished
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/printJob/printed")]
        public IActionResult Post([FromQuery] Guid jobId)
        {
            m_logger.LogDebug("Setting Print Job {0} to PRINTED", jobId);


            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                var job = context.PrintJobs.Find(jobId);

                if(job == default)
                {
                    m_logger.LogError("Could not locate Print Job {0}", jobId);

                    return NotFound();
                }

                if(job.Status != PrintJobStatus.PRINTING)
                {
                    m_logger.LogError("Print Job {0} already set to PRINTED", jobId);

                    return Conflict();
                }

                m_logger.LogDebug("Print Job {0} set to PRINTED", jobId);


                job.Status = PrintJobStatus.PRINTED;
            }

            return Ok();
        }
    }
}