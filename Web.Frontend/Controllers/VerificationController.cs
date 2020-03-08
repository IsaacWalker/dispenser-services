using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Frontend.SchedulerService;

namespace Web.Frontend.Controllers
{
    public class VerificationController : AFrontendControllerBase
    {
        public VerificationController(ISchedulerClient schedulerClient) : base(schedulerClient)
        {
        }

        public IActionResult Verification()
        {
            return View("Views/Pages/Verification.cshtml");
        }
    }
}