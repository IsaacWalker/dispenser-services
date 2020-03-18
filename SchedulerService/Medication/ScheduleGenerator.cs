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


        public Task<WeeklyPrescriptionSchedule> Run(DateTime date, IList<Prescription> prescriptions)
        {
            WeeklyPrescriptionSchedule schedule = new WeeklyPrescriptionSchedule();
            schedule.StartDate = date;

            /// Inspect the frequency of each prescription
            /// Create the ODF's for each occurence
            /// Split them into DailySchedules for each day of week
            /// Compose the daily schedules of printjobs containing ODF's
            
            throw new NotImplementedException();
        }
    }
}
