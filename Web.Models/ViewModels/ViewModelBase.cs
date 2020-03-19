using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.ViewModels
{
    public abstract class ViewModelBase
    {
        /// <summary>
        /// The nurse Id
        /// </summary>
        public Guid NurseId { get; set; }


        /// <summary>
        /// First name of nurse
        /// </summary>
        public string NurseFirstName { get; set; }


        /// <summary>
        /// Last Name of Nurse
        /// </summary>
        public string NurseLastName { get; set; }


        /// <summary>
        /// Navbar Model
        /// </summary>
        public NavbarPartialModel NavbarModel { get; set; }
    }
}
