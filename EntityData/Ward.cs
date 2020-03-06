using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// A ward
    /// </summary>
    public class Ward : AEntityBase
    {
        /// <summary>
        /// The name of the ward
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The rooms of this ward
        /// </summary>
        public virtual IList<Room> Rooms { get; set; } = new List<Room>();
    }
}
