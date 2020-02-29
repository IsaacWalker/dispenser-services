using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// A time when a particualr prescription should be taken
    /// </summary>
    public class PrescriptionTime
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// Time of the day when prescription should be taken
        /// </summary>
        public DateTime Time { get; set; }


        /// <summary>
        /// Presciption  Id that its associated with
        /// </summary>
        public Guid PrescriptionId { get; set; }


        /// <summary>
        /// Prescription Navigation property
        /// </summary>
        public virtual Prescription Prescription { get; set; }
    }
}
