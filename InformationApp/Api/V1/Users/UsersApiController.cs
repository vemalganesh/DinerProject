using InformationApp.contracts;
using InformationApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.Api.V1.Users
{   
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersApiController : Controller
    {
        private List<ApplicationUser> _users;

        [HttpGet(ApiRoutes.Users.GetUsersList)]
        public ApplicationUser GetAllUsers() {  

            ApplicationUser users = new ApplicationUser();
            users.Email = "It works"; 
            return users;
        }


        //[HttpPost(ApiRoutes.Users.GetUsersList)]
        //public IActionResult CreateUsers()
        //{

        //     ApplicationUser users = new ApplicationUser();


        //    return Created();
        //}
    }
}
