using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SchedulerService.Controllers
{
    public abstract class AMobileControllerBase : ControllerBase
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
        public AMobileControllerBase(IServiceProvider serviceProvider, ILogger logger)
        {
            m_serviceProvider = serviceProvider;
            m_logger = logger;
        }
    }
}
