using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    public class Nurse : AEntityBase
    {
        /// <summary>
        /// Nurse First Name
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// Nurse Last Name
        /// </summary>
        public string LastName { get; set; }
    }
}
