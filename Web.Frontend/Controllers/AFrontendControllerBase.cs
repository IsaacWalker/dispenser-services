using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Frontend.SchedulerService;

namespace Web.Frontend.Controllers
{
    /// <summary>
    /// Base controller for Frontend
    /// </summary>
    public abstract class AFrontendControllerBase : Controller
    {
        /// <summary>
        /// Client for the scheduler service
        /// </summary>
        protected readonly ISchedulerClient m_schedulerClient;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="schedulerClient"></param>
        public AFrontendControllerBase(ISchedulerClient schedulerClient)
        {
            m_schedulerClient = schedulerClient;
        }
    }
}
