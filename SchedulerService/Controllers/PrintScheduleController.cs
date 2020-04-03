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
                model.Day = dayOfWeek.ToString();
                model.Date = DateTime.Now.Date.ToString("dd/MM");
                Tuple<string, string> dayDate = new Tuple<string, string>(model.Day, model.Date);
                model.WeekDays = new List<Tuple<string, string>>
                {
                    dayDate
                };
                for (double i = 1.0 ; i < 7.0; i++)
                {
                    var d = DateTime.Now.AddDays(i);
                    dayDate = new Tuple<string, string>(d.DayOfWeek.ToString(), d.Date.ToString("dd/MM"));
                    model.WeekDays.Add(dayDate);
                }

                var week = context.WeeklyPrescriptionSchedules
                    .OrderByDescending(W => W.StartDate)
                    .FirstOrDefault();

                // Get the print jobs for today
                var printJobs = context.PrintJobs
                    .Include(PJ => PJ.DailySchedule)
                    .Where(PJ => PJ.DailySchedule.WeeklyPrescriptionScheduleId == week.Id
                        && PJ.DailySchedule.Date.DayOfWeek == dayOfWeek)
                    .Include(PJ => PJ.ODFs)
                    .ThenInclude(O => O.Prescription)
                    .ThenInclude(P => P.Patient)
                    .ToList();

                // Divide Them into "Active" or "Queued" based on status
                    var batches = printJobs.GroupBy(PJ => PJ.Status,PJ => new Batch()
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

                model.ActiveBatch = batches.Where(G => G.Key == PrintJobStatus.PRINTED || G.Key == PrintJobStatus.PRINTING)
                    .SelectMany(G => G.ToList())
                    .FirstOrDefault();

                model.QueuedBatches = batches.Where(G => G.Key != PrintJobStatus.PRINTED && G.Key != PrintJobStatus.PRINTING)
                    .SelectMany(G => G.ToList())
                    .ToList();

                return View("~/Views/Pages/PrintSchedule.cshtml", model);
            }
        }
    }
}