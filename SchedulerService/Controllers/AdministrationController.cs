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
using Web.Models;
using Web.Models.ViewModels;

namespace Web.SchedulerService.Controllers
{
    [ApiController]
    public class AdministrationController : APIControllerBase
    {
        public AdministrationController(IServiceProvider serviceProvider, ILogger logger) : base(serviceProvider, logger)
        {
        }


        /// <summary>
        /// Gets the view of 
        /// </summary>
        /// <param name="nurseId"></param>
        /// <param name="odfId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/view/administration")]
        public async Task<IActionResult> Get([FromQuery] Guid nurseId, [FromQuery] Guid odfId)
        {
            using (var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                // Get Nurse
                Nurse administiringNurse = await context.Nurses.FindAsync(nurseId);

                var query = context.ODFs
                     .Where(O => O.Id == odfId)
                     .Include(O => O.PrescriptionTime)
                     .ThenInclude(PT => PT.Prescription)
                     .ThenInclude(P => P.Patient)
                     .Select(O => new
                     {
                         patientFirstName = O.PrescriptionTime.Prescription.Patient.FirstName,
                         patientlastName = O.PrescriptionTime.Prescription.Patient.LastName,
                         medicationName = O.PrescriptionTime.Prescription.DrugName,
                         dosage = O.PrescriptionTime.Prescription.Dosage,
                         dateOfBirth = O.PrescriptionTime.Prescription.Patient.DateOfBirth
                     })
                     .FirstOrDefault();

                AdministrationVerificationModel model = new AdministrationVerificationModel()
                {
                    NurseFirstName = administiringNurse.FirstName,
                    NurseLastName = administiringNurse.LastName,
                    PatientFirstName = query.patientFirstName,
                    PatientLastName = query.patientlastName,
                    Dosage = query.dosage,
                    MedicationName = query.medicationName,
                    PatientDateOfBirth = query.dateOfBirth,
                    CurrentTime = DateTime.Now
                };

                return Ok(model);
            }
        }


        /// <summary>
        /// Confirms an administration
        /// </summary>
        /// <param name="confirmModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/administration/confirm")]
        public async Task<IActionResult> ConfirmAdministration([FromBody] ConfirmAdministrationModel confirmModel)
        {
            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                ODFAdministration administration = new ODFAdministration
                {
                    DateTime = confirmModel.AdministrationTime,
                    NurseId = confirmModel.NurseId,
                    ODFId = confirmModel.OdfId
                };

                // Add an ODF administration
                context.ODFAdministrations.Add(administration);
                await context.SaveChangesAsync();

                return Ok();
            }
        }
    }
}