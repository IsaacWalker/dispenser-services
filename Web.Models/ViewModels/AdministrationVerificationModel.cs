using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.ViewModels
{
    public class AdministrationVerificationModel : ViewModelBase
    {
        public Guid OdfId { get; set; }

        public string MedicationName { get; set; }

        public float Dosage { get; set; }

        public Guid PatientId { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }

        public DateTime PatientDateOfBirth { get; set; }

        public DateTime CurrentTime { get; set; }

        public string NurseFirstName { get; set; }

        public string NurseLastName { get; set; }
    }
}
