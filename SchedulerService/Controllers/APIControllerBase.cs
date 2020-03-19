using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;
using Web.Models.ViewModels;

namespace Web.SchedulerService.Controllers
{
    public abstract class APIControllerBase : ControllerBase
    {
        /// <summary>
        /// Service Provider
        /// </summary>
        protected readonly IServiceProvider m_serviceProvider;


        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger m_logger;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public APIControllerBase(IServiceProvider serviceProvider, ILogger logger)
        {
            m_serviceProvider = serviceProvider;
            m_logger = logger;
        }


        protected async virtual Task InitializeViewModel<T>(Guid nurseId, ServiceDbContext context, T model)
            where T : ViewModelBase
        {
            Nurse administiringNurse = await context.Nurses.FindAsync(nurseId);

            var patients = await context.Patients.Select(P => new NavbarPatientModel()
            {
                FirstName = P.FirstName,
                LastName = P.LastName,
                PatientId = P.Id
            })
            .ToListAsync();

            model.NavbarModel = new NavbarPartialModel()
            {
                NurseFirstName = administiringNurse.FirstName,
                NurseLastName = administiringNurse.LastName,
                NurseId = nurseId,
                Patients = patients
            };
        }
    }
}
