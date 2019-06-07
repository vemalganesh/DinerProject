using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InformationApp.Models;
using Microsoft.Extensions.Configuration;

namespace InformationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration config) {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            //string connstr = configuration.GetConnectionString("DefaultConnection");

            CompanyDataAccesLayer cd = new CompanyDataAccesLayer(configuration); 
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
