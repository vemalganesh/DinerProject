using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationApp.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration Configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
            typeof(IInstaller).IsAssignableFrom(x) && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, Configuration));
        }

        public static void InstallConfigureServicesInAssembly(this IApplicationBuilder app,  IConfiguration Configuration,  IHostingEnvironment env)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
            typeof(IConfigureServiceInstaller).IsAssignableFrom(x) && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IConfigureServiceInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(Configuration, app, env));
        }
    }
}
