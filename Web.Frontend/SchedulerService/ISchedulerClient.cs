using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.ViewModels;

namespace Web.Frontend.SchedulerService
{
    /// <summary>
    /// Scheduler client
    /// </summary>
    public interface ISchedulerClient
    {
        /// <summary>
        /// Gets the home page model
        /// </summary>
        /// <param name="nurseId"></param>
        /// <returns></returns>
        public Task<HomePageModel> GetHomePageModel(Guid nurseId);


        /// <summary>
        /// Gets the patient info model
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public Task<PatientInformationPageModel> GetPatientInfoModel(Guid patientInfo);
    }
}
