using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;

namespace Web.SchedulerService
{
    /// <summary>
    /// Generates mock Data
    /// </summary>
    public static class MockData
    {
        private static volatile bool _isInitialized = false;


        public static void AddMockData(IServiceProvider serviceProvider)
        {
            if(!_isInitialized)
            {
                Initialize(serviceProvider);
            }
        }


        private static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                Nurse nurse = new Nurse()
                {
                    Id = Guid.Parse("a85b1827-33c5-4d45-8e11-7cb51ede0e59"),
                    FirstName = "Nurse",
                    LastName = "Ratchet"
                };

                context.Add(nurse);

                var p1 = AddPatient(context, "John", "Doe", 1.78f, DateTime.Parse("04-06-1992"), 67.0f);
               // var p2 = AddPatient(context, "Jane", "Doe", 1.65f, DateTime.Parse("06-02-1996"), 45.0f);

                context.SaveChanges();

                //TODO - Add prescriptions 
                Prescription johnPrescriptionOne = new Prescription()
                {
                    Drug = "DrugName",
                    Dosage = 1.25f,
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.Date + TimeSpan.FromDays(14.0)
                };

                AddPrescription(context, johnPrescriptionOne, new List<DateTime> { DateTime.Parse("09:00"), DateTime.Parse("14:00") });
                context.SaveChanges();

                //TODO - Assign prescriptions to Patients
                p1.Prescriptions.Add(johnPrescriptionOne);

                context.SaveChanges();

            }
        }

        private static Patient AddPatient(ServiceDbContext context, string firstName, string lastName, float height, DateTime dob, float weight)
        {
            Patient p = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                Height = height,
                DateOfBirth = dob,
                Weight = weight
            };

            context.Patients.Add(p);

            return p;
        }

        private static void AddPrescription(ServiceDbContext context, Prescription prescription, IList<DateTime> times)
        {
            context.Prescriptions.Add(prescription);
            context.SaveChanges();

            foreach (var time in times)
            {
                PrescriptionTime prescriptionTime = new PrescriptionTime()
                {
                    Time = time
                };

                prescriptionTime.Prescription = prescription;
                context.PrescriptionTimes.Add(prescriptionTime);
            }

            context.SaveChanges();
        }
    }
}
