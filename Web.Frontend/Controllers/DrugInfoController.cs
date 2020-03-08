using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Frontend.SchedulerService;

namespace Web.Frontend.Controllers
{
    public class DrugInfoController : AFrontendControllerBase
    {
        public DrugInfoController(ISchedulerClient schedulerClient) : base(schedulerClient)
        {
        }


        [Route("drug")]
        public async Task<IActionResult> DrugInfo([FromQuery] Guid prescriptionId )
        {
            var model = await m_schedulerClient.GetDrugInfoModel(prescriptionId);
            return View("Views/Pages/DrugInfo.cshtml",model);
        }
    }
}