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
    public abstract class APIControllerBase : Controller
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


        protected async virtual Task<T> InitializeViewModel<T>(ServiceDbContext context)
            where T : ViewModelBase, new()
        {
            var nurse = context.Nurses.Find(Guid.Parse(User.Claims.FirstOrDefault().Value));

            T model = new T();

            var patients = await context.Patients.Select(P => new NavbarPatientModel()
            {
                FirstName = P.FirstName,
                LastName = P.LastName,
                PatientId = P.Id
            })
            .ToListAsync();

            model.NavbarModel = new NavbarPartialModel()
            {
                NurseFirstName = nurse.FirstName,
                NurseLastName = nurse.LastName,
                Patients = patients
            };

            return model;
        }
    }
}
