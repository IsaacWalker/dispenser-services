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
        [Route("patient")]
        public async Task<ViewResult> Get([FromQuery] Guid patientId)
        {
            using(var scope = m_serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();
                PatientInformationPageModel model = await InitializeViewModel<PatientInformationPageModel>(context);

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
                        ward = P.Bed.Room.Ward.Name,
                        dateOfBirth = P.DateOfBirth
                    })
                    .FirstOrDefault();
                

                if(patientInfo == default)
                {
                    return View();
                }

                model.FirstName = patientInfo.firstName;
                model.LastName = patientInfo.lastName;
                model.Room = patientInfo.room;
                model.Bed = patientInfo.bed;
                model.Ward = patientInfo.ward;
                model.PatientId = patientId;
                model.DateOfBirth = patientInfo.dateOfBirth;

                /// Get the ODFs from ODFAdministrations for today
                var administeredMedications = context.ODFAdministrations
                    .Where(A => A.DateTime.Date == DateTime.Now.Date)
                    .Include(A => A.ODF)
                    .ThenInclude(P => P.Prescription)
                    .Where(A => A.ODF.Prescription.PatientId == patientId)
                    .Include(A => A.Nurse)
                    .Select(A => new AdministeredMedication()
                    {
                        Dosage = A.ODF.Prescription.Dosage,
                        DrugName = A.ODF.Prescription.DrugName,
                        NurseName = A.Nurse.FirstName,
                        Time = A.DateTime,
                        OdfId = A.ODFId
                    })
                    .ToList();

                /// Get ODF's for today not associated with an administration
                var pendingMedications = context.ODFs
                    .Include(O => O.ODFAdministration)
                    .Where(O => O.ODFAdministration == default)
                    .Include(PT => PT.Prescription)
                    .Where(O => O.Prescription.PatientId == patientId)
                    .Include(O => O.PrintJob)
                    .Select(O => new PendingMedication()
                    {
                        DrugName = O.Prescription.DrugName,
                        Dosage = O.Prescription.Dosage,
                        Status = O.PrintJob.Status.ToString(),
                        OdfId = O.Id,
                        Time = O.PrintJob.ExpectedTimeOfReadiness
                    }).ToList();

                model.AdministeredMedications = administeredMedications;
                model.PendingMedications = pendingMedications;


                return View("Views/Pages/PatientInfo.cshtml", model);
            }
        }
    }
}