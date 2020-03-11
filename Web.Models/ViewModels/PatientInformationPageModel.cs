using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    /// <summary>
    /// Model for the patient information page
    /// </summary>
    public class PatientInformationPageModel : ViewModelBase
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        public Guid PatientId { get; set; }


        /// <summary>
        /// The Patients First Name
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// The Surname of the Patient
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// Bed
        /// </summary>
        public string Bed { get; set; }


        /// <summary>
        /// Room 
        /// </summary>
        public string Room { get; set; }


        /// <summary>
        /// Ward
        /// </summary>
        public string Ward { get; set; }


        /// <summary>
        /// Pending Medications for that day
        /// </summary>
        public IList<PendingMedication> PendingMedications { get; set; }


        /// <summary>
        /// Medications already administered that day
        /// </summary>
        public IList<AdministeredMedication> AdministeredMedications { get; set; }
    }

    public class PendingMedication : DailyMedicationModel
    {
        /// <summary>
        /// Current Status of the drug
        /// </summary>
        public string Status { get; set; }
    }

    public class AdministeredMedication : DailyMedicationModel
    {
        /// <summary>
        /// The name of the nurse who administered the drug
        /// </summary>
        public string NurseName { get; set; }
    }

    public abstract class DailyMedicationModel
    {
        /// <summary>
        /// Id of the ODF
        /// </summary>
        public Guid OdfId { get; set; }


        /// <summary>
        /// The Name of the drug
        /// </summary>
        public string DrugName { get; set; }


        /// <summary>
        /// The Dosage of the Drug
        /// </summary>
        public double Dosage { get; set; }


        /// <summary>
        /// The time of administrations
        /// </summary>
        public DateTime Time { get; set; }
    }

}
