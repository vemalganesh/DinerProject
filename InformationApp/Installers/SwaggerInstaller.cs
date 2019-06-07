using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InformationApp.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InformationApp.Installers
{
    public class SwaggerInstaller : IConfigureServiceInstaller
    {
        public void InstallServices(IConfiguration Configuration,IApplicationBuilder app, IHostingEnvironment env)
        {
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(Options => { Options.RouteTemplate = swaggerOptions.JsonRoute; });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Descirption);
            });
        }
    }
}
