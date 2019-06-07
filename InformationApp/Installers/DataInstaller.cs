using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InformationApp.Data;
using InformationApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InformationApp.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultTokenProviders();

            //// Add application services.
            //services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
           


        }
    }
}
