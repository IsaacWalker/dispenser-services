using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    public class Prescription
    {
        public int Id { get; set; }


        public string Drug { get; set; }


        public int PatientId { get; set; }


        public Patient Patient { get; set; }
    }
}
