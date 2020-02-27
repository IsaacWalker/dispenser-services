using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SchedulerService.Models
{
    /// <summary>
    /// Medication Model
    /// </summary>
    public sealed class MedicationModel
    {
        /// <summary>
        /// Name of the drug
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Dosage 
        /// </summary>
        public double Dosage { get; set; }


        /// <summary>
        /// Route of administration
        /// </summary>
        public string Route { get; set; }


        /// <summary>
        /// Notes
        /// </summary>
        public string Note { get; set; }
    }
}
