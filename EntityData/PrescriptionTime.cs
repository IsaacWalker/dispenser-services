using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// A time when a particualr prescription should be taken
    /// </summary>
    public class PrescriptionTime : AEntityBase
    {
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


        /// <summary>
        /// The ODFs that are printed for this time
        /// </summary>
        public virtual IList<ODF> ODFs { get; set; } = new List<ODF>();
    }
}
