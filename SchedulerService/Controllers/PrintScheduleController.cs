using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.EntityData;
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
        public async Task<ViewResult> Index()
        {
            using (var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();
                PrintScheduleModel model = await InitializeViewModel<PrintScheduleModel>(context);

                var batches = context.PrintJobs
                    .Include(PJ => PJ.DailySchedule)
                    .Where(PJ => PJ.DailySchedule.Date == DateTime.Today.Date)
                    .Select(PJ => new Batch()
                    {
                        ETA = PJ.ExpectedTimeOfReadiness,
                        Status = PJ.Status.ToString(),
                        PrintJobId = PJ.Id,
                        BatchNumber = PJ.BatchNumber
                    })
                    .ToList();

                model.Batches = batches;

                return View("~/Views/Pages/PrintSchedule.cshtml", model);
            }
        }
    }
}