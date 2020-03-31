using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;

namespace Web.SchedulerService.Medication
{
    public sealed class ScheduleGenerator : IScheduleGenerator
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration m_configuration;


        /// <summary>
        /// Service Provider
        /// </summary>
        private readonly IServiceProvider m_serviceProvider;


        /// <summary>
        /// Cosntructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="serviceProvider"></param>
        public ScheduleGenerator(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            m_configuration = configuration;
            m_serviceProvider = serviceProvider;
            BatchSize = m_configuration.GetValue<int>("OdfSettings:BatchSize");
            AdministrationStartTime = m_configuration.GetValue<DateTime>("ScheduleSettings:AdminStartTime");
            AdministrationTimeStep = TimeSpan.FromMinutes(m_configuration.GetValue<double>("ScheduleSettings:AdminTimeStep"));
            AdministrationPreparationTime = TimeSpan.FromMinutes(m_configuration.GetValue<double>("ScheduleSettings:AdminPrepTime"));
        }


        /// <summary>
        /// Runs the schedule generator
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="prescriptions"></param>
        /// <returns></returns>
        public async Task<WeeklyPrescriptionSchedule> Run(DateTime startDate, IList<Prescription> prescriptions)
        {
            WeeklyPrescriptionSchedule weekSchedule = new WeeklyPrescriptionSchedule
            {
                StartDate = DateTime.Now.Date,
            };

            IList<DailySchedule> days = new List<DailySchedule>();

            using (var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                for (var dayDate = weekSchedule.StartDate;
                    dayDate < weekSchedule.StartDate + TimeSpan.FromDays(7);
                    dayDate += TimeSpan.FromDays(1)
                    )
                {
                    days.Add(new DailySchedule()
                    {
                        Date = dayDate,
                        WeeklyPrescriptionSchedule = weekSchedule,
                        PrintJobs = new List<PrintJob>()
                    });
                }

                /// Inspect the frequency of each prescription
                foreach (var prescription in prescriptions)
                {
                    int dailyOccurences, step;

                    switch (prescription.Frequency)
                    {

                        case (Frequency.DAILY):
                            dailyOccurences = 1;
                            step = 1;
                            break;
                        case (Frequency.BID):
                            dailyOccurences = 2;
                            step = 1;
                            break;
                        case (Frequency.OTHER_DAY):
                            dailyOccurences = 1;
                            step = 2;
                            break;
                        case (Frequency.Q3H):
                            dailyOccurences = 4;
                            step = 1;
                            break;
                        case (Frequency.Q4H):
                            dailyOccurences = 3;
                            step = 1;
                            break;
                        case (Frequency.Q5H):
                            dailyOccurences = 2;
                            step = 1;
                            break;
                        case (Frequency.QHS):
                            dailyOccurences = 8;
                            step = 1;
                            break;
                        case (Frequency.QID):
                            dailyOccurences = 4;
                            step = 1;
                            break;
                        case (Frequency.QWK):
                            dailyOccurences = 1;
                            step = 7;
                            break;
                        case (Frequency.TID):
                            dailyOccurences = 3;
                            step = 1;
                            break;
                        default:
                            throw new Exception();
                    }

                    /// Go through each day of the week
                    for (int dayInd = 0; dayInd < days.Count; dayInd += step)
                    {
                        int printJobNum = 0;

                        /// Go through each time-independent occurence slot
                        for (int odfInd = 0; odfInd < dailyOccurences; odfInd++)
                        {

                            // Move the JobNumber to where there is a Job with less than BatchSize odfs
                            while (days[dayInd].PrintJobs.Count() != printJobNum && days[dayInd].PrintJobs[printJobNum].ODFs.Count == BatchSize)
                            {
                                printJobNum++;
                            }


                            PrintJob printJob = (days[dayInd].PrintJobs.Count() == printJobNum) ? new PrintJob()
                            {
                                BatchNumber = (uint)printJobNum,
                                Status = PrintJobStatus.UNINITIALIZED,
                                ODFs = new List<ODF>(),
                                DailySchedule = days[dayInd]
                            } : days[dayInd].PrintJobs[printJobNum];

                            context.PrintJobs.Add(printJob);

                            ODF odf = new ODF()
                            {
                                DateTimeOfCreation = DateTime.Now,
                                Prescription = prescription
                            };

                            printJob.ODFs.Add(odf);
                            printJobNum++;
                        }
                    }
                }


                /// Sort each print job into times for each day
                foreach (DailySchedule dailySchedule in days)
                {
                    DateTime time = AdministrationStartTime;
                    foreach (PrintJob job in dailySchedule.PrintJobs)
                    {
                        job.ExpectedTimeOfReadiness = time - AdministrationPreparationTime;
                        time += AdministrationTimeStep;
                    }
                }

                var pjs = weekSchedule.DaySchedules.Where(D => D.Date.Date == DateTime.Now.Date)
                .FirstOrDefault()
                .PrintJobs;

                var pj = pjs //.Where(PJ => PJ.ExpectedTimeOfReadiness.Hour - DateTime.Now.Hour >= 0)
                 .OrderBy(PJ => PJ.ExpectedTimeOfReadiness.Hour - DateTime.Now.Hour)
                .FirstOrDefault()
                .Status = PrintJobStatus.PRINTING;

                context.WeeklyPrescriptionSchedules.Add(weekSchedule);
                await context.SaveChangesAsync();

                return weekSchedule;
            }
        }


        /// <summary>
        /// The Maximum size of a batch
        /// </summary>
        private readonly int BatchSize;


        /// <summary>
        /// The start time of the administration
        /// </summary>
        private readonly DateTime AdministrationStartTime;


        /// <summary>
        /// The step at which administrations should occur
        /// </summary>
        private readonly TimeSpan AdministrationTimeStep;


        /// <summary>
        /// Leeway given to fetch the medication before administering
        /// </summary>
        private readonly TimeSpan AdministrationPreparationTime; 
    }

}
