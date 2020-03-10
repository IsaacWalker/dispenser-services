using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    /// <summary>
    /// Model for the Home Page
    /// </summary>
    public class HomePageModel : ViewModelBase
    {
        /// <summary>
        /// The Name of the nurse
        /// </summary>
        public string NurseName { get; set; }


        /// <summary>
        /// Batch Number
        /// </summary>
        public int BatchNumber { get; set; }


        /// <summary>
        /// The Job (Batch) Id
        /// </summary>
        public Guid PrintJobId { get; set; }


        /// <summary>
        /// ODFs of that batch
        /// </summary>
        public IList<BatchODF> ODFs { get; set; }
    }


    public class BatchODF
    {
        /// <summary>
        /// The Name of the Patient
        /// </summary>
        public string PatientName { get; set; }


        /// <summary>
        /// Id of the patient
        /// </summary>
        public Guid PatientId { get; set; }


        /// <summary>
        /// Id of the prescription for this ODF
        /// </summary>
        public Guid PrescriptionId { get; set; }



        /// <summary>
        /// Name of the Medication
        /// </summary>
        public string MedicationName { get; set; }


        /// <summary>
        /// Id of the ODF of that patient
        /// </summary>
        public Guid ODFId { get; set; }
    }

}
