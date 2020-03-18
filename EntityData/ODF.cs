using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// Orodispersable Film is a printed instantiation of a PrescriptionTime
    /// </summary>
    public class ODF : AEntityBase
    {
        /// <summary>
        /// The Creation DateTime for this ODF
        /// </summary>
        public DateTime DateTimeOfCreation { get; set; }


        /// <summary>
        /// Id of the prescription that this ODF was associated with
        /// </summary>

        public Guid PrescriptionId { get; set; }


        /// <summary>
        /// Navigation property of the Prescription 
        /// </summary>
        public virtual Prescription Prescription { get; set; }


        /// <summary>
        /// Print Job Id
        /// </summary>
        public Guid PrintJobId { get; set; }


        /// <summary>
        /// Print Jobs associated
        /// </summary>
        public virtual PrintJob PrintJob { get; set; }


        /// <summary>
        /// The Id of the Administration of this ODF
        /// </summary>
        public Guid ODFAdministrationId { get; set; }


        /// <summary>
        /// The Administration of the is ODF
        /// </summary>
        public virtual ODFAdministration ODFAdministration { get; set; }
    }
}
