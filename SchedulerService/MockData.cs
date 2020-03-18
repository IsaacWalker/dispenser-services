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
                var p2 = AddPatient(context, "Jane", "Doe", 1.65f, DateTime.Parse("06-02-1996"), 45.0f);
                var p3 = AddPatient(context, "Chad", "Chadwick", 1.78f, DateTime.Parse("23-06-1997"), 61.0f);
                var p4 = AddPatient(context, "Brad", "Philips", 1.79f, DateTime.Parse("16-11-1993"), 75.0f);
                var p5 = AddPatient(context, "Terrence", "Davis", 1.68f, DateTime.Parse("02-12-1965"), 66.0f);

                context.SaveChanges();

                Prescription p1PrescriptionOne = new Prescription()
                {
                    DrugName = "Colace",
                    Dosage = 25f,
                    StartDate = DateTime.Now.Date - TimeSpan.FromDays(2.0),
                    EndDate = DateTime.Now.Date + TimeSpan.FromDays(14.0),
                    Route = "PO",
                    Patient = p1,
                    Frequency = Frequency.BID
                };

                Prescription p2PrescriptionOne = new Prescription()
                {
                    DrugName = "Zofran",
                    Dosage = 80f,
                    StartDate = DateTime.Now.Date - TimeSpan.FromDays(2.0),
                    EndDate = DateTime.Now.Date + TimeSpan.FromDays(14.0),
                    Route = "PO",
                    Patient = p2,
                    Frequency = Frequency.BID
                };

                Prescription p3PrescriptionOne = new Prescription()
                {
                    DrugName = "Desipramine",
                    Dosage = 125f,
                    StartDate = DateTime.Now.Date - TimeSpan.FromDays(2.0),
                    EndDate = DateTime.Now.Date + TimeSpan.FromDays(12.0),
                    Route = "PO",
                    Patient = p3,
                    Frequency = Frequency.BID
                };


                Prescription p4PrescriptionOne = new Prescription()
                {
                    DrugName = "Oxycoton",
                    Dosage = 100f,
                    StartDate = DateTime.Now.Date - TimeSpan.FromDays(2.0),
                    EndDate = DateTime.Now.Date + TimeSpan.FromDays(12.0),
                    Route = "PO",
                    Patient = p4,
                    Frequency = Frequency.BID
                };


                Prescription p5PrescriptionOne = new Prescription()
                {
                    DrugName = "Morphine",
                    Dosage = 90f,
                    StartDate = DateTime.Now.Date - TimeSpan.FromDays(2.0),
                    EndDate = DateTime.Now.Date + TimeSpan.FromDays(12.0),
                    Route = "PO",
                    Patient = p5,
                    Frequency = Frequency.BID
                };

                context.Add(p1PrescriptionOne);
                context.Add(p2PrescriptionOne);
                context.Add(p3PrescriptionOne);
                context.Add(p4PrescriptionOne);
                context.Add(p5PrescriptionOne);
                context.SaveChanges();

                
                PrintJob job = new PrintJob()
                {
                    Status = PrintJobStatus.REMOVED
                };

                PrintJob job2 = new PrintJob()
                {
                    Status = PrintJobStatus.PRINTED
                };

                context.PrintJobs.Add(job);

                ODF odf1 = new ODF()
                {
                    Prescription = p1PrescriptionOne,
                    PrintJob = job2
                };

                ODF odf2 = new ODF()
                {
                    Prescription = p2PrescriptionOne,
                    PrintJob = job2
                };

                ODF odf3 = new ODF()
                {
                    Prescription = p3PrescriptionOne,
                    PrintJob = job
                };

                ODF odf4 = new ODF()
                {
                    Prescription= p4PrescriptionOne,
                    PrintJob = job2
                };

                ODF odf5 = new ODF()
                {
                    Prescription = p5PrescriptionOne,
                    PrintJob = job2
                };

                ODF odf6 = new ODF()
                {
                    Prescription = p3PrescriptionOne,
                    PrintJob = job2
                };


                context.Add(odf1);
                context.Add(odf2);
                context.Add(odf3);
                context.Add(odf4);
                context.Add(odf5);
                context.Add(odf6);

                context.SaveChanges();

                // Add administrations
                ODFAdministration adminOne = new ODFAdministration 
                { DateTime = odf3.DateTimeOfCreation + TimeSpan.FromMinutes(100.0), NurseId = nurse.Id, ODF = odf3};
                context.ODFAdministrations.Add(adminOne);

                // Add Wards
                Bed b1 = new Bed { Label = "A1" };
                Bed b2 = new Bed { Label = "A2" };
                Bed b3 = new Bed { Label = "A3" };
                Bed b4 = new Bed { Label = "A4" };
                Bed b5 = new Bed { Label = "A5" };

                context.Beds.Add(b1);
                context.Beds.Add(b2);
                context.Beds.Add(b3);
                context.Beds.Add(b4);
                context.Beds.Add(b5);

                p1.Bed = b1;
                p2.Bed = b2;
                p3.Bed = b3;
                p4.Bed = b4;
                p5.Bed = b5;

                Room room = new Room { Label = "LG12" };
                room.Beds.Add(b1);
                room.Beds.Add(b2);
                room.Beds.Add(b3);
                room.Beds.Add(b4);
                room.Beds.Add(b5);
                context.Rooms.Add(room);

                Ward ward = new Ward { Name = "Pediatric ward" };
                ward.Rooms.Add(room);
                context.Wards.Add(ward);

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

    }
}
