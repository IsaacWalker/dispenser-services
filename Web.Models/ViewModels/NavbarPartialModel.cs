using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.ViewModels
{
    public sealed class NavbarPartialModel
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
        /// Patients
        /// </summary>
        public List<NavbarPatientModel> Patients { get; set; }
    }

    public sealed class NavbarPatientModel
    {
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// Surname
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// Id
        /// </summary>
        public Guid PatientId { get; set; }
    }

}
