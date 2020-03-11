using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Frontend.SchedulerService;
using Web.Models.ViewModels;

namespace Web.Frontend.Controllers
{
    public class AdministrationController : AFrontendControllerBase
    {
        public AdministrationController(ISchedulerClient schedulerClient) : base(schedulerClient)
        {
        }


        [Route("administration")]
        public async Task<IActionResult> Administration([FromQuery] Guid nurseId, [FromQuery] Guid odfId)
        {
            AdministrationVerificationModel model = await m_schedulerClient.GetAdministrationModel(nurseId, odfId);
            return View("Views/Pages/Administration.cshtml",model);
        }

        [Route("administration/confirm")]
        public async Task<IActionResult> Confirm([FromQuery] Guid nurseId, [FromQuery] Guid odfId, [FromQuery] Guid patientId)
        {
            bool successful = await m_schedulerClient.ConfirmAdministration(nurseId, odfId, DateTime.Now);

            return Redirect(string.Format("/patient?{0}={1}&{2}={3}", nameof(nurseId), nurseId, nameof(patientId), patientId));
        }
    }
}