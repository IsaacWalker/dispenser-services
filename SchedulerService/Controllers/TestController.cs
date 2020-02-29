using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.EntityData;
using Web.SchedulerService.Medication;

namespace Web.SchedulerService.Controllers
{
    /// STUB - used for testing gRPC cpommunication
    
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {


        private readonly IODFGenerator m_generator;


        public TestController(IODFGenerator generator)
        {
            m_generator = generator;
        }


        [HttpGet()]
        public IActionResult Get()
        {
            return Ok();
        }

    }
}
