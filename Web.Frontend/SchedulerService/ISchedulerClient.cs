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
        public Task<PatientInformationPageModel> GetPatientInfoModel(Guid nurseId,Guid patientInfo);


        /// <summary>
        /// Gets the Information model about a view
        /// </summary>
        /// <param name="prescriptionId"></param>
        /// <returns></returns>
        public Task<DrugInformationPageModel> GetDrugInfoModel(Guid nurseId,Guid prescriptionId);


        /// <summary>
        /// Confirms an administration
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="odfId"></param>
        /// <returns></returns>
        public Task<bool> ConfirmAdministration(Guid nurseId, Guid odfId, DateTime administrationTime);


        /// <summary>
        /// Gets the view for the administation screen
        /// </summary>
        /// <param name="nurseId"></param>
        /// <param name="odfId"></param>
        /// <returns></returns>
        public Task<AdministrationVerificationModel> GetAdministrationModel(Guid nurseId, Guid odfId);
    }
}
