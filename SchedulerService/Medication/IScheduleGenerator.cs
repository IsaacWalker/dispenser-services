using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;

namespace Web.SchedulerService.Medication
{
    public interface IScheduleGenerator
    {
        public Task<WeeklyPrescriptionSchedule> Run(DateTime date, IList<Prescription> prescriptions);
    }
}
