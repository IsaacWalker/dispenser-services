using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        public async Task<ViewResult> Index([FromQuery] string day)
        {
            if(!Enum.TryParse<DayOfWeek>(day, out DayOfWeek dayOfWeek))
            {
                dayOfWeek = DateTime.Now.DayOfWeek;
            }

            using (var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();
                PrintScheduleModel model = await InitializeViewModel<PrintScheduleModel>(context);
                model.Today = DateTime.Now.DayOfWeek;

                var week = context.WeeklyPrescriptionSchedules
                    .OrderByDescending(W => W.StartDate)
                    .FirstOrDefault();
                    

                // Get the print jobs for the week
                var printJobs = context.PrintJobs
                    .Include(PJ => PJ.DailySchedule)
                    .Where(PJ => PJ.DailySchedule.WeeklyPrescriptionScheduleId == week.Id)
                    .Include(PJ => PJ.ODFs)
                    .ThenInclude(O => O.Prescription)
                    .ThenInclude(P => P.Patient)
                    .ToList();

                // Group them into days
                IDictionary<DayOfWeek, ScheduleDay> scheduleDays = printJobs
                    .GroupBy(PJ => PJ.DailySchedule.Date.DayOfWeek, PJ => PJ)
                    .ToDictionary(G => G.Key, G => 
                    {
                        var dailyPrintJobs = G.ToList();

                        // Divide Them into "Active" or "Queued" based on status
                        var batches = dailyPrintJobs.GroupBy(PJ => PJ.Status, PJ => new Batch()
                        {
                            BatchNumber = PJ.BatchNumber,
                            ETA = PJ.ExpectedTimeOfReadiness,
                            Status = PJ.Status.ToString(),
                            PrintJobId = PJ.Id,
                            ODFs = PJ.ODFs.Select(O => new ODFModel()
                            {
                                DrugName = O.Prescription.DrugName,
                                Dosage = O.Prescription.Dosage,
                                PatentLabel = O.Prescription.Patient.FirstName + " " + O.Prescription.Patient.FirstName
                            }).ToList()
                        })
                        .ToList(); // Now in "Active" and "NonActive" groups


                        return new ScheduleDay()
                        {
                            Day = G.Key.ToString(),
                            ActiveBatch = batches.Where(G => G.Key == PrintJobStatus.PRINTED || G.Key == PrintJobStatus.PRINTING)
                                .SelectMany(G => G.ToList())
                                .FirstOrDefault(),
                            QueuedBatches = batches.Where(G => G.Key != PrintJobStatus.PRINTED && G.Key != PrintJobStatus.PRINTING)
                                 .SelectMany(G => G.ToList())
                                .ToList()
                        };
                    });


                model.ScheduleDays = scheduleDays;
                return View("~/Views/Pages/PrintSchedule.cshtml", model);
            }
        }
    }
}