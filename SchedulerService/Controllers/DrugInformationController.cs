﻿using System;
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
    public class DrugInformationController : APIControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public DrugInformationController(IServiceProvider serviceProvider, ILogger<DrugInformationController> logger) 
            : base(serviceProvider, logger)
        {
        }


        /// <summary>
        /// Gets the model for the drug information screen
        /// </summary>
        /// <param name="prescriptionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/view/[controller]")]
        public IActionResult Get([FromQuery] Guid nurseId,[FromQuery] Guid prescriptionId)
        {
            // TODO - finish query
            m_logger.LogInformation("Getting Drug Information screen for {0}", prescriptionId);

            DrugInformationPageModel drugInformationPageModel = new DrugInformationPageModel();

            using (var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                Prescription prescription = context.Prescriptions.Find(prescriptionId);
                Nurse nurse = context.Nurses.Find(nurseId);

                if(prescription == default)
                {
                    return NotFound();
                }

                drugInformationPageModel.DrugName = prescription.DrugName;
                drugInformationPageModel.Notes = prescription.Notes;
                drugInformationPageModel.Dosage = prescription.Dosage;
                drugInformationPageModel.Route = prescription.Route;
                drugInformationPageModel.NurseId = nurseId;
                drugInformationPageModel.NurseFirstName = nurse.FirstName;
                drugInformationPageModel.NurseLastName = nurse.LastName;

                var pastAdministrations = context.ODFAdministrations
                    .Include(A => A.ODF)
                    .ThenInclude(ODF => ODF.PrescriptionTime)
                    .Where(A => A.ODF.PrescriptionTime.PrescriptionId == prescriptionId)
                    .Include(A => A.Nurse)
                    .Select(A => new PastAdministrationModel()
                    {
                        AdministeringNurse = A.Nurse.FirstName + " " + A.Nurse.LastName,
                        DateTime = A.DateTime,
                        Dosage = prescription.Dosage,
                        ExpirationDate = prescription.EndDate
                    })
                    .ToList();

                drugInformationPageModel.PastAdministrationModels = pastAdministrations;

            }

            return Ok(drugInformationPageModel);
        }
    }
}