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
        public int Id { get; set; }


        /// <summary>
        /// Name of the drug
        /// </summary>
        public string Drug { get; set; }


        /// <summary>
        /// Id of the patient of which the drug is administered
        /// </summary>
        public int PatientId { get; set; }


        /// <summary>
        /// Patient of which the drug is administered
        /// </summary>
        public Patient Patient { get; set; }
    }
}
