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
        public Guid Id { get; set; }


        /// <summary>
        /// Patient first name
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// Patient last name
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// Weight in Kgs's
        /// </summary>
        public float Weight { get; set; }


        /// <summary>
        /// Height in cm's
        /// </summary>
        public float Height { get; set; }


        /// <summary>
        /// DateOfBirth
        /// </summary>
        public DateTime DateOfBirth { get; set; }



        /// <summary>
        /// The prescriptions associated with the patient
        /// </summary>
        public virtual IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
