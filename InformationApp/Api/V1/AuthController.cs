using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InformationApp.contracts;
using InformationApp.Models;
using InformationApp.Models.AccountViewModel;
using InformationApp.Models.AuthViewModel;
using InformationApp.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InformationApp.Api
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost(ApiRoutes.Auth.register)]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var authResponse = await _identityService.RegisterAsync(model);

            if (!authResponse.Success)
            { 
                return BadRequest(new AuthFailResponseModel
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessModel
            {
                Token = authResponse.Token
            });
        }

        [HttpPost(ApiRoutes.Auth.logIn)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var authResponse = await _identityService.LoginAsync(model);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailResponseModel
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessModel
            {
                Token = authResponse.Token
            });
        }
    }
}
