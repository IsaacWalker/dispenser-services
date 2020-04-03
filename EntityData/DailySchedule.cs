using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{ 
    public class DailySchedule : AEntityBase
    {
        public DateTime Date { get; set; }


        /// <summary>
        /// Id of the weekly Prescription Id
        /// </summary>
        public Guid WeeklyPrescriptionScheduleId { get; set; }


        /// <summary>
        /// Weekly schedule
        /// </summary>
        public virtual WeeklyPrescriptionSchedule WeeklyPrescriptionSchedule { get; set; }


        /// <summary>
        /// Print Jobs
        /// </summary>
        public virtual IList<PrintJob> PrintJobs { get; set; }
    }
}
