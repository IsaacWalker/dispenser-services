using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SchedulerService.Models
{
    /// <summary>
    /// Model for Patient
    /// </summary>
    public sealed class PatientModel
    {
        /// <summary>
        /// Patients First Name
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// Patients Last Name
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// Patients Date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
