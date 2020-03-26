using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;

namespace Web.SchedulerService.Medication
{
    public sealed class ScheduleGenerator : IScheduleGenerator
    {
        private readonly IConfiguration m_configuration;


        private int BatchSize => m_configuration.GetValue<int>("OdfSettings:BatchSize");


        public ScheduleGenerator(IConfiguration configuration)
        {
            m_configuration = configuration;
        }


        public Task<WeeklyPrescriptionSchedule> Run(DateTime startDate, IList<Prescription> prescriptions)
        {
            WeeklyPrescriptionSchedule weekSchedule = new WeeklyPrescriptionSchedule
            {
                StartDate = DateTime.Now.Date,        
            };

            IList<DailySchedule> days = new List<DailySchedule>();

            for (var dayDate = weekSchedule.StartDate;
                dayDate < weekSchedule.StartDate + TimeSpan.FromDays(7);
                dayDate += TimeSpan.FromDays(1))
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

                for (int dayInd = 0; dayInd < days.Count; dayInd += step)
                {
                    for (int odfInd = 0; odfInd < dailyOccurences; odfInd++)
                    {
                        PrintJob printJob = days[dayInd].PrintJobs[odfInd] ?? new PrintJob()
                        {
                            BatchNumber = (uint)dayInd,
                            Status = PrintJobStatus.UNINITIALIZED,
                            ODFs = new List<ODF>(),
                            DailySchedule = days[dayInd]
                        };

                        ODF odf = new ODF()
                        {
                            DateTimeOfCreation = DateTime.Now,
                            Prescription = prescription
                        };
                    }
                }
            }

            /// Create the ODF's for each occurence
            /// Split them into DailySchedules for each day of week
            /// Compose the daily schedules of printjobs containing ODF's
            
            throw new NotImplementedException();
        }
    }
}
