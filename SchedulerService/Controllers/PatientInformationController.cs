using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.EntityData;
using Web.Models.ViewModels;

namespace Web.SchedulerService.Controllers
{
    [ApiController]
    public class PatientInformationController : APIControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public PatientInformationController(IServiceProvider serviceProvider, ILogger<PatientInformationController> logger) 
            : base(serviceProvider, logger)
        {
        }


        /// <summary>
        /// Gets the model for the patient information screen
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/view/[controller]")]
        public IActionResult Get([FromQuery] Guid patientId)
        {
            PatientInformationPageModel model = new PatientInformationPageModel();

            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                var patientInfo = context.Patients
                    .Where(P => P.Id == patientId)
                    .Include(P => P.Bed)
                    .ThenInclude(B => B.Room)
                    .ThenInclude(R => R.Ward)
                    .Select(P => new
                    {
                        firstName = P.FirstName,
                        lastName = P.LastName,
                        bed = P.Bed.Label,
                        room = P.Bed.Room.Label,
                        ward = P.Bed.Room.Ward.Name
                    })
                    .FirstOrDefault();
                

                if(patientId == default)
                {
                    return NotFound(patientId);
                }

                model.FirstName = patientInfo.firstName;
                model.LastName = patientInfo.lastName;
                model.Room = patientInfo.room;
                model.Bed = patientInfo.bed;
                model.Ward = patientInfo.ward;

                var administeredMedications = context.ODFs
                    .Where(O => O.PrescriptionTime.Prescription.PatientId == patientId)
                    .Include(O => O.PrescriptionTime.Prescription)
                    .Select(O => new AdministeredMedication()
                    {
                        Dosage = O.PrescriptionTime.Prescription.Dosage,
                        DrugName = O.PrescriptionTime.Prescription.DrugName,
                        NurseName = O.ODFAdministration.Nurse.FirstName,
                        Time = O.ODFAdministration.DateTime
                    });

                model.AdministeredMedications = administeredMedications.ToList();
            }

            return Ok(model);
        }
    }
}