using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    public class WeeklyPrescriptionSchedule : AEntityBase
    {
        /// <summary>
        /// Start Time
        /// </summary>
        public DateTime StartDate { get; set; }


        /// <summary>
        /// Schedueles for each day of the week
        /// </summary>
        public virtual IList<DailySchedule> DaySchedules { get; set; }
    }
}
