﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    /// <summary>
    /// Model for the Home Page
    /// </summary>
    public class HomePageModel : ViewModelBase
    {
        /// <summary>
        /// Batch Number
        /// </summary>
        public int BatchNumber { get; set; }


        /// <summary>
        /// The Job (Batch) Id
        /// </summary>
        public Guid PrintJobId { get; set; }


        /// <summary>
        /// Status of the batch
        /// </summary>
        public string Status { get; set; }


        /// <summary>
        /// The expected time of completion
        /// </summary>
        public DateTime ETA { get; set; }


        /// <summary>
        /// ODFs of that batch
        /// </summary>
        public IList<BatchODF> ODFs { get; set; }
    }


    public class BatchODF
    {
        /// <summary>
        /// The Name of the Patient
        /// </summary>
        public string PatientName { get; set; }


        /// <summary>
        /// Id of the patient
        /// </summary>
        public Guid PatientId { get; set; }


        /// <summary>
        /// Id of the prescription for this ODF
        /// </summary>
        public Guid PrescriptionId { get; set; }



        /// <summary>
        /// Name of the Medication
        /// </summary>
        public string MedicationName { get; set; }


        /// <summary>
        /// Id of the ODF of that patient
        /// </summary>
        public Guid ODFId { get; set; }


        /// <summary>
        /// The patients bed
        /// </summary>
        public string PatientBed { get; set; }


        /// <summary>
        /// Patient room
        /// </summary>
        public string PatientRoom { get; set; }


        /// <summary>
        /// The Patient Ward
        /// </summary>
        public string PatientWard { get; set; }


        /// <summary>
        /// Dosage
        /// </summary>
        public double Dosage { get; set; }
    }

}
