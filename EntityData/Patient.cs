using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    public class Patient
    {
        public int Id { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public virtual IList<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
