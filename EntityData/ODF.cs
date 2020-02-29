using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// Orodispersable Film is a printed instantiation of a PrescriptionTime
    /// </summary>
    public class ODF
    {
        /// <summary>
        /// Id of the ODF
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// Id of the prescription time that this ODF was associated with
        /// </summary>

        public Guid PrescriptionTimeId { get; set; }


        /// <summary>
        /// Navigation property of the Prescription Time
        /// </summary>
        public virtual PrescriptionTime PrescriptionTime { get; set; }


        /// <summary>
        /// Print Job Id
        /// </summary>
        public Guid PrintJobId { get; set; }


        /// <summary>
        /// Print Jobs associated
        /// </summary>
        public virtual PrintJob PrintJob { get; set; }
    }
}
