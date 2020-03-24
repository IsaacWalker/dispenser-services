using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    public class Nurse : IdentityUser<Guid>
    {
        public Nurse()
        {
          
        }


        /// <summary>
        /// Nurse First Name
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// Nurse Last Name
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// Administrations
        /// </summary>
        public IList<ODFAdministration> Administrations { get; set; }
    }
}
