using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
        public class PrintScheduleModel : ViewModelBase
        {
            /// <summary>
            /// ODFs of that batch
            /// </summary>
            public IList<Batch> Batches { get; set; }
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

        }

}