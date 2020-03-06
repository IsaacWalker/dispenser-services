using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    public class Room : AEntityBase
    {
        /// <summary>
        /// The label displayed on the room
        /// </summary>
        public string Label { get; set; }


        /// <summary>
        /// The Id of the ward this room is part of
        /// </summary>
        public Guid WardId { get; set; }


        /// <summary>
        /// The Ward this room is in
        /// </summary>
        public virtual Ward Ward { get; set; }


        /// <summary>
        /// Beds navigation property
        /// </summary>
        public virtual IList<Bed> Beds { get; set; }
    }
}
