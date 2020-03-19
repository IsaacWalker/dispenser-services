using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.ViewModels
{
    public sealed class NavbarPartialModel
    {
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
