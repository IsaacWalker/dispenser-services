using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models.ViewModels;

namespace Web.SchedulerService.Controllers
{
    [Authorize]
    public class PrintScheduleController : APIControllerBase
    {
        public PrintScheduleController(IServiceProvider serviceProvider, ILogger<PrintScheduleController> logger) 
            : base(serviceProvider, logger)
        {
        }


        [Route("schedule")]
        public ViewResult Index()
        {
            HomePageModel model = new HomePageModel()
            {
                BatchNumber = 5,
                Status = "PRINTING",
                ETA = DateTime.Now,
                ODFs = new List<BatchODF>(),
                NavbarModel = new NavbarPartialModel()
                {
                    Patients = new List<NavbarPatientModel>()
                },
                PrintJobId = Guid.NewGuid()
            };

            return View("~/Views/Pages/PrintSchedule.cshtml", model);
        }
    }
}