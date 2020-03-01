using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    public abstract class AEntityBase
    {
        /// <summary>
        /// Guid Id used by all entities
        /// </summary>
        public Guid Id { get; set; }
    }
}
