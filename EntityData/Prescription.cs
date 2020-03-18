using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// Prescription
    /// </summary>
    public class Prescription : AEntityBase
    {
        /// <summary>
        /// Name of the drug
        /// </summary>
        public string DrugName { get; set; }


        /// <summary>
        /// Dosage in Mg
        /// </summary>
        public float Dosage { get; set; }


        /// <summary>
        /// Route of administration
        /// </summary>
        public string Route { get; set; }


        /// <summary>
        /// The indication of the drug
        /// </summary>
        public string Indication { get; set; }


        /// <summary>
        /// Notes about the drug
        /// </summary>
        public string Notes { get; set; }


        /// <summary> 
        /// Frequency Abbreviation
        /// </summary>
        public Frequency Frequency { get; set; }


        /// <summary>
        /// First date drug is taken on
        /// </summary>
        public DateTime StartDate { get; set; }


        /// <summary>
        /// Last date drug is taken on
        /// </summary>
        public DateTime EndDate { get; set; }


        /// <summary>
        /// Id of the patient of which the drug is administered
        /// </summary>
        public Guid PatientId { get; set; }


        /// <summary>
        /// Patient of which the drug is administered
        /// </summary>
        public virtual Patient Patient { get; set; }


        /// <summary>
        /// The ODFs which we're printed for this prescription
        /// </summary>
        public virtual IList<ODF> ODFs { get; set; }
    }

}
