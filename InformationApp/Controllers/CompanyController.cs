using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InformationApp.Models;
using Microsoft.Extensions.Configuration;
using InformationApp.DataAccessLayer;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InformationApp.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly CompanyDataAccesLayer objcompany;
        public CompanyController(IConfiguration config)
        {
            this.configuration = config;
            objcompany = new CompanyDataAccesLayer(configuration);
        }
        

        // GET: /<controller>/
        public IActionResult Index()
        {
            var lstCompany = objcompany.GetAllCompany().ToList();

            return View(lstCompany);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Company company)
        {
            if (ModelState.IsValid)
            {
                objcompany.AddCompany(company);
                return RedirectToAction("Index");
            }
            return View(company);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Company company = objcompany.GetCompanyData(id);

            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind]Company company)
        {
            if (id != company.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objcompany.UpdateCompany(company);
                return RedirectToAction("Index");
            }
            return View(company);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Company company = objcompany.GetCompanyData(id);

            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Company company = objcompany.GetCompanyData(id);

            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            objcompany.DeleteCompany(id);
            return RedirectToAction("Index");
        }
    }

  
}


