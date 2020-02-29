using Microsoft.AspNetCore.Mvc;
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
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public AMobileControllerBase(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
        }
    }
}
