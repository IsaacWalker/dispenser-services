﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    public class PrintScheduleModel : ViewModelBase
    {
        public IDictionary<DayOfWeek, ScheduleDay> ScheduleDays { get; set; }


        public DayOfWeek Today { get; set; }
    }


    public class ScheduleDay
    {
        public string Day { get; set; }


        /// <summary>
        /// The printing or printed batch
        /// </summary>
        public Batch ActiveBatch { get; set; }


        /// <summary>
        /// ODFs of that batch
        /// </summary>
        public IList<Batch> QueuedBatches { get; set; }
    }



    public class Batch
    {
        /// <summary>
        /// Batch Number
        /// </summary>
        public uint BatchNumber { get; set; }


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
        /// ODFs
        /// </summary>
        public IList<ODFModel> ODFs { get; set; }

    }

    public class ODFModel
    {
        /// <summary>
        /// Drug Name
        /// </summary>
        public string DrugName { get; set; }


        /// <summary>
        /// Dosage
        /// </summary>
        public float Dosage { get; set; }


        /// <summary>
        /// Patient
        /// </summary>
        public string PatentLabel { get; set; }
    }
}