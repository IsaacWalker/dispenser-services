using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;
using Web.Models.ViewModels;

namespace Web.SchedulerService.Controllers
{
    [Authorize]
    public class HomeController : APIControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public HomeController(IServiceProvider serviceProvider, ILogger<HomeController> logger) : base(serviceProvider, logger)
        {
        }


        /// <summary>
        /// Gets the model for the Home of the app
        /// </summary>
        /// <param name="nurseId"></param>
        /// <returns></returns>
        [Route("[controller]")]
        [Route("")]
        public async Task<ViewResult> Home()
        {
            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();
                HomePageModel model = await InitializeViewModel<HomePageModel>(context);


                var currentJob = context.PrintJobs
                    .Where(Job => Job.Status == PrintJobStatus.PRINTING || Job.Status == PrintJobStatus.PRINTED)
                    .FirstOrDefault();
                
                if (currentJob != null)
                {
                    var result = context.ODFs.Where(O => O.PrintJobId == currentJob.Id)
                        .Include(O => O.Prescription)
                        .ThenInclude(P => P.Patient)
                        .ThenInclude(P => P.Bed)
                        .ThenInclude(B => B.Room)
                        .ThenInclude(R => R.Ward)
                        .Select(O => new 
                        { 
                            patientName = O.Prescription.Patient.FirstName + " " + O.Prescription.Patient.LastName,
                            drugName = O.Prescription.DrugName,
                            odfId = O.Id,
                            patientId = O.Prescription.Patient.Id,
                            prescriptionId = O.PrescriptionId,
                            bed = O.Prescription.Patient.Bed.Label,
                            room =  O.Prescription.Patient.Bed.Room.Label,
                            ward = O.Prescription.Patient.Bed.Room.Ward.Name,
                            dosage = O.Prescription.Dosage
                        });

                    var batchModels = result.Select(R => new BatchODF()
                    {
                        PatientId = R.patientId,
                        PatientName = R.patientName,
                        MedicationName = R.drugName,
                        ODFId = R.odfId,
                        PrescriptionId = R.prescriptionId,
                        PatientBed = R.bed,
                        PatientRoom = R.room,
                        PatientWard = R.ward,
                        Dosage = R.dosage
                    });

                    model.PrintJobId = currentJob.Id;
                    model.ODFs = batchModels.ToList();
                    model.Status = currentJob.Status.ToString();
                }
                else
                {
                    model.ODFs = new List<BatchODF>();
                }

                return View("Views/Home/Index.cshtml", model);
            }
        }
    }
}
