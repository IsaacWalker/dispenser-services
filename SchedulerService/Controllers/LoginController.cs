using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.EntityData;
using Web.Models;
using Web.Models.ViewModels;

namespace Web.SchedulerService.Controllers
{
    [AllowAnonymous]
    public class LoginController : APIControllerBase
    {
        private readonly UserManager<Nurse> m_userManager;


        private readonly SignInManager<Nurse> m_signInManager;


        public LoginController(IServiceProvider serviceProvider, 
            ILogger<LoginController> logger,
            UserManager<Nurse> userManager,
            SignInManager<Nurse> signInManager) : base(serviceProvider, logger)
        {
            m_userManager = userManager;
            m_signInManager = signInManager;
        }


        [Route("login")]
        public ViewResult Login()
        {
            return View("Views/Pages/Login.cshtml");
        }


        [Route("login/attempt")]
        [HttpPost]
        public async Task<IActionResult> Attempt(LoginAttemptModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await m_signInManager.PasswordSignInAsync(model.InputEmailAddress, model.InputPassword, model.RememberPasswordCheck, false);

                if(result.Succeeded)
                {
                    return Redirect("/home");
                }
            }
            
            return View("Views/Pages/Login.cshtml", new LoginPageModel() { SigninSuccess = false });
        }
    }
}