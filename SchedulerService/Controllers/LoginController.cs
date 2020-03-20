using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.SchedulerService.Controllers
{
    public class LoginController : APIControllerBase
    {
        public LoginController(IServiceProvider serviceProvider, ILogger<LoginController> logger) : base(serviceProvider, logger)
        {
        }


        [Route("login")]
        public ViewResult Login()
        {
            return View("Views/Pages/Login.cshtml");
        }


        [Route("login/attempt")]
        [HttpPost]
        public IActionResult Attempt(LoginAttemptModel model)
        {
            return Ok();
        }
    }
}