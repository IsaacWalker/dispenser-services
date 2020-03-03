using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SchedulerService.Models.PageModels
{
    /// <summary>
    /// Page model for the Drug Information View
    /// </summary>
    public class DrugInformationPageModel
    {
        /// <summary>
        /// The name of the drug
        /// </summary>
        public string DrugName { get; set; }


        /// <summary>
        /// The Name of the Patient
        /// </summary>
        public string PatientName { get; set; }


        /// <summary>
        /// The Status of the that drug
        /// </summary>
        public string Status { get; set; }


        /// <summary>
        /// The dosage 
        /// </summary>
        public float Dosage { get; set; }


        /// <summary>
        /// The Route of Administration
        /// </summary>
        public string Route { get; set; }


        /// <summary>
        /// The Indication of the Drug
        /// </summary>
        public string Indication { get; set; }


        /// <summary>
        /// The Maximum Administration Frequency of that Drug
        /// </summary>
        public int MaxFrequency { get; set; }


        /// <summary>
        /// Any additional notes of the prescription
        /// </summary>
        public string Notes { get; set; }

    }
}
