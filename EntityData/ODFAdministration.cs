using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// An Administation of an ODF
    /// </summary>
    public class ODFAdministration : AEntityBase
    {

        /// <summary>
        /// The time at which administration occurred
        /// </summary>
        public DateTime DateTime { get; set; }


        /// <summary>
        /// Id of the ODF
        /// </summary>
        public Guid ODFId { get; set; }


        /// <summary>
        /// The ODF Administered
        /// </summary>
        public virtual ODF ODF { get; set; }


        /// <summary>
        /// The Id of the Nurse who administered it
        /// </summary>
        public Guid NurseId { get; set; }


        /// <summary>
        /// The Nurse who Administered the ODF
        /// </summary>
        public virtual Nurse Nurse { get; set; }
    }
}
