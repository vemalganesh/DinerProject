using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InformationApp.DataAccessLayer;
using InformationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InformationApp.Controllers
{
    public class OutletController : Controller
    {
        private readonly IConfiguration configuration;
        private OutletDataAccessLayer objOutletModel;
        private CompanyDataAccesLayer objCompanyModel;
        public OutletController(IConfiguration config)
        {
            this.configuration = config;
            objOutletModel = new OutletDataAccessLayer(configuration);
            objCompanyModel = new CompanyDataAccesLayer(configuration);

        }
        // GET: /<controller>/
        public IActionResult Index(string id)
        {
            List<OutletModel> lstOutletModel = new List<OutletModel>();
            lstOutletModel = objOutletModel.GetAllOutlet(id).ToList();
            TempData["CompanyId"] = id;
            return View(lstOutletModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] OutletModel OutletModel)
        {
            OutletModel.Company_Id = TempData["CompanyId"].ToString();
            if (ModelState.IsValid)
            {
                objOutletModel.AddOutlet(OutletModel);
                return RedirectToAction("Index", new { id = OutletModel.Company_Id });
            }
            return View(OutletModel);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OutletModel OutletModel = objOutletModel.GetOutletData(id);

            if (OutletModel == null)
            {
                return NotFound();
            }
            return View(OutletModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind]OutletModel OutletModel)
        {
            if (id != OutletModel.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                OutletModel.Company_Id = TempData["CompanyId"].ToString();
                objOutletModel.UpdateOutlet(OutletModel);
                return RedirectToAction("Index", new { id = OutletModel.Company_Id });
            }
            return View(OutletModel);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OutletModel OutletModel = objOutletModel.GetOutletData(id);

            if (OutletModel == null)
            {
                return NotFound();
            }
            return View(OutletModel);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OutletModel OutletModel = objOutletModel.GetOutletData(id);

            if (OutletModel == null)
            {
                return NotFound();
            }
            return View(OutletModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            string CompId = TempData["CompanyId"].ToString();
            objOutletModel.DeleteOutlet(id);
            return RedirectToAction("Index", new { id = CompId });
        }
    }


}
