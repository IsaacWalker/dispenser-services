using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// Patient Entity
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Patient first name
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// Patient last name
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// The prescriptions associated with the patient
        /// </summary>
        public virtual IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
