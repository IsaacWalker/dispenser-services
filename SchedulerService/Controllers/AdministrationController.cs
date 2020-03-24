using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class AdministrationController : APIControllerBase
    {
        public AdministrationController(IServiceProvider serviceProvider, ILogger<AdministrationController> logger) : base(serviceProvider, logger)
        {
        }


        /// <summary>
        /// Gets the view of 
        /// </summary>
        /// <param name="nurseId"></param>
        /// <param name="odfId"></param>
        /// <returns></returns>
        [Route("administration")]
        public async Task<ViewResult> Get([FromQuery] Guid odfId)
        {
            using (var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();
                
                var query = context.ODFs
                     .Where(O => O.Id == odfId)
                     .Include(O => O.Prescription)
                     .ThenInclude(P => P.Patient)
                     .Select(O => new
                     {
                         patientFirstName = O.Prescription.Patient.FirstName,
                         patientlastName = O.Prescription.Patient.LastName,
                         patientId = O.Prescription.PatientId,
                         medicationName = O.Prescription.DrugName,
                         dosage = O.Prescription.Dosage,
                         dateOfBirth = O.Prescription.Patient.DateOfBirth
                     })
                     .FirstOrDefault();

                AdministrationVerificationModel model = await InitializeViewModel<AdministrationVerificationModel>(context);

                model.PatientFirstName = query.patientFirstName;
                model.PatientLastName = query.patientlastName;
                model.Dosage = query.dosage;
                model.MedicationName = query.medicationName;
                model.PatientDateOfBirth = query.dateOfBirth;
                model.CurrentTime = DateTime.Now;
                model.PatientId = query.patientId;
                model.OdfId = odfId; 
            
                return View("Views/Pages/Administration.cshtml", model);
            }
        }


        /// <summary>
        /// Confirms an administration
        /// </summary>
        /// <param name="confirmModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("administration/confirm")]
        public async Task<IActionResult> Confirm(ConfirmAdministrationModel confirmModel)
        {
            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                var odf = context.ODFAdministrations.Find(confirmModel.OdfId);

                if(odf != default)
                {
                    return NotFound();
                }

                ODFAdministration administration = new ODFAdministration
                {
                    DateTime = confirmModel.AdministrationTime,
                    NurseId = Guid.Parse(User.Claims.FirstOrDefault().Value),
                    ODFId = confirmModel.OdfId
                };


                // Add an ODF administration
                context.ODFAdministrations.Add(administration);
                await context.SaveChangesAsync();
                return Redirect("~/home");
            }
        }
    }
}