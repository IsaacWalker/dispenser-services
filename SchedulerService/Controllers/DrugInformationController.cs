﻿using System;
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
using Web.Models.ViewModels;

namespace Web.SchedulerService.Controllers
{
    [Authorize]
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
        [Route("drug")]
        public async Task<ViewResult> Get([FromQuery] Guid prescriptionId)
        {
            // TODO - finish query
            m_logger.LogInformation("Getting Drug Information screen for {0}", prescriptionId);

 
            using (var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();
                DrugInformationPageModel drugInformationPageModel = await InitializeViewModel<DrugInformationPageModel>(context);

                var prescription = context.Prescriptions.Where(P => P.Id == prescriptionId)
                    .Include(P => P.Patient)
                    .Select(P => new {
                    PatientName = P.Patient.FirstName + " " + P.Patient.LastName,
                        P.DrugName,
                        P.Notes,
                        P.Dosage,
                        P.Route,
                        P.EndDate,
                        P.Prescriber,
                        P.Frequency
                    } )
                    .FirstOrDefault();

                if(prescription == default)
                {
                    return View();
                }

                drugInformationPageModel.PatientName = prescription.PatientName;
                drugInformationPageModel.DrugName = prescription.DrugName;
                drugInformationPageModel.Notes = prescription.Notes;
                drugInformationPageModel.Dosage = prescription.Dosage;
                drugInformationPageModel.Route = prescription.Route;
                drugInformationPageModel.Prescriber = prescription.Prescriber;
                drugInformationPageModel.Frequency = prescription.Frequency.ToString();

                var pastAdministrations = context.ODFAdministrations
                    .Include(A => A.ODF)
                    .Where(A => A.ODF.PrescriptionId == prescriptionId)
                    .Include(A => A.Nurse)
                    .Select(A => new PastAdministrationModel()
                    {
                        AdministeringNurse = A.Nurse.FirstName + " " + A.Nurse.LastName,
                        DateTime = A.DateTime,
                        Dosage = prescription.Dosage,
                        ExpirationDate = prescription.EndDate,
                        BatchNumber = A.ODF.PrintJob.BatchNumber
                    })
                    .ToList();

                drugInformationPageModel.PastAdministrationModels = pastAdministrations;

                return View("Views/Pages/DrugInfo.cshtml", drugInformationPageModel);
            }
        }
    }
}