using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// A bed in a hospital
    /// </summary>
    public class Bed : AEntityBase
    {
        /// <summary>
        /// The label displayed on the bed
        /// </summary>
        public string Label { get; set; }


        /// <summary>
        /// The Id of the room
        /// </summary>
        public Guid RoomId { get; set; }


        /// <summary>
        /// The room this bed is associated with
        /// </summary>
        public virtual Room Room { get; set; }


        /// <summary>
        /// The Id of the patient at this bed
        /// </summary>
        public Guid PatientId { get; set; }


        /// <summary>
        /// The patient nav property at this bed
        /// </summary>
        public virtual Patient Patient { get; set; }
    }
}
