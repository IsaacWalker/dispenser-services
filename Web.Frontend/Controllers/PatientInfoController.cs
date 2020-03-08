using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Frontend.SchedulerService;

namespace Web.Frontend.Controllers
{
    public class PatientInfoController : AFrontendControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="schedulerClient"></param>
        public PatientInfoController(ISchedulerClient schedulerClient) : base(schedulerClient)
        {

        }


        [Route("patient")]
        public async Task<ViewResult> PatientInfo([FromQuery] Guid patientId)
        {
            var model = await m_schedulerClient.GetPatientInfoModel(patientId);
            return View("Views/Pages/PatientInfo.cshtml", model);
        }
    }
}