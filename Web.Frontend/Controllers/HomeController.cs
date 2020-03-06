using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Frontend.SchedulerService;
using Web.Models.ViewModels;

namespace Web.Frontend.Controllers
{
    /// <summary>
    /// Controller for the home screen
    /// </summary>
    public class HomeController : AFrontendControllerBase
    {
        private static readonly Guid nurseId = Guid.Parse("a85b1827-33c5-4d45-8e11-7cb51ede0e59");


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="schedulerClient"></param>
        public HomeController(ISchedulerClient schedulerClient) : base(schedulerClient)
        {
        }


        /// <summary>
        /// Index screen
        /// </summary>
        /// <returns></returns>
        [Route("index")]
        public async Task<ViewResult> Index()
        {
            var homePageModel = await m_schedulerClient.GetHomePageModel(nurseId);
            return View(homePageModel);
        }
    }
}