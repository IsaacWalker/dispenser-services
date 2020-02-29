using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// Prescription
    /// </summary>
    public class Prescription
    {
        /// <summary>
        /// Prescription Id
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// Name of the drug
        /// </summary>
        public string Drug { get; set; }


        /// <summary>
        /// Dosage in Mg
        /// </summary>
        public float Dosage { get; set; }


        /// <summary>
        /// Times in the day where the medication is to be taken
        /// </summary>
        public virtual IList<DateTime> Times { get; set; } = new List<DateTime>();


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
        public Patient Patient { get; set; }
    }
}
