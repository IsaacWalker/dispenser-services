using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.SchedulerService.Controllers
{
    public abstract class ServiceControllerBase : ControllerBase
    {
        /// <summary>
        /// Service Provider
        /// </summary>
        protected readonly IServiceProvider m_serviceProvider;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ServiceControllerBase(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
        }
    }
}
