﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InformationApp.DataAccessLayer;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InformationApp.Models
{
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;
        private UserDataAccessLayer objUserModel;
        public UserController(IConfiguration config)
        {
                this.configuration = config;
                objUserModel = new UserDataAccessLayer(configuration);
         
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            List<ApplicationUser> lstUserModel = new List<ApplicationUser>();
            lstUserModel = objUserModel.GetAllUser().ToList();

            return View(lstUserModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] ApplicationUser UserModel)
        {
            if (ModelState.IsValid)
            {
                objUserModel.AddUser(UserModel);
                return RedirectToAction("Index");
            }
            return View(UserModel);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser UserModel = objUserModel.GetUserData(id);

            if (UserModel == null)
            {
                return NotFound();
            }
            return View(UserModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind]ApplicationUser UserModel)
        {
            if (id != UserModel.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objUserModel.UpdateUser(UserModel);
                return RedirectToAction("Index");
            }
            return View(UserModel);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser UserModel = objUserModel.GetUserData(id);

            if (UserModel == null)
            {
                return NotFound();
            }
            return View(UserModel);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser UserModel = objUserModel.GetUserData(id);

            if (UserModel == null)
            {
                return NotFound();
            }
            return View(UserModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            objUserModel.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
