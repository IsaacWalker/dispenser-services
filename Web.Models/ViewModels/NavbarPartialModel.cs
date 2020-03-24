using System;
using System.Collections.Generic;
using System.Text;
using Web.EntityData;

namespace Web.Models.ViewModels
{
    public sealed class NavbarPartialModel
    {
        /// <summary>
        /// Nurse First Name
        /// </summary>
        public string NurseFirstName { get; set; }


        /// <summary>
        /// Nurse Last Name
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
