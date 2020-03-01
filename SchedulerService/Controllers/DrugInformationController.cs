using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.EntityData;
using Web.SchedulerService.Models.PageModels;

namespace Web.SchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugInformationController : AMobileControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public DrugInformationController(IServiceProvider serviceProvider, ILogger<DrugInformationController> logger) 
            : base(serviceProvider, logger)
        {
        }


        /// <summary>
        /// Gets the model for the drug information screen
        /// </summary>
        /// <param name="prescriptionId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] Guid prescriptionId)
        {
            // TODO - finish query

            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                DrugInformationPageModel drugInformationPageModel = new DrugInformationPageModel();
                return Ok(drugInformationPageModel);
            }
        }
    }
}