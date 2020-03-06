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
    public class PatientInformationController : AMobileControllerBase
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
        [Route("api/[controller]")]
        public IActionResult Get([FromQuery] Guid patientId)
        {
            PatientInformationPageModel model = new PatientInformationPageModel();

            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                Patient patient = context.Patients.Find(patientId);

                if(patientId == default)
                {
                    return NotFound(patientId);
                }

                model.FirstName = patient.FirstName;
                model.Surname = patient.LastName;

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
            }

            return Ok(model);
        }
    }
}