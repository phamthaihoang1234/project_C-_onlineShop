﻿using Ictshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Ictshop.Areas.Admin.Controllers
{
    public class AuthorizationController : Controller
    {
        ShopManagement db = new ShopManagement();
        // GET: Admin/Authorization
 
        public ActionResult ListRoles()
        {
            var model = db.Roles.ToList();
            return View(model);
        }
        public ActionResult ListByRoleID(int RoleID)
        {
            ViewBag.RoleID = RoleID;
            TempData["FunctionCode"] = db.Functions.ToList();
            ViewBag.ListPermissonByRoleID = db.Permissions.Where(p => p.RoleId == RoleID).ToList();
            TempData["ListPermissonByRoleID"] = db.Permissions.Where(p => p.RoleId == RoleID) .ToList();
            var Functions = db.Functions.ToList();
            return View(Functions);
        }

        [HttpPost]
        public ActionResult UpdateRole()
        {
            int RoleID = Int32.Parse(Request.Form["RoleID"]);
            db.Permissions.RemoveRange(db.Permissions.Where(p => p.RoleId == RoleID));
            db.SaveChanges();
            try
                {
                Permission permission = new Permission();
                string[] permissons = Request.Form.GetValues("FunctionCode");
                foreach (string item in permissons)
                {
                    permission.RoleId = RoleID;
                    permission.FunctionCode = item;
                    db.Permissions.Add(permission);
                    db.SaveChanges();
                    db.Entry(permission).State = EntityState.Detached;
                }
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }    
            TempData["ListPermissonByRoleID"] = db.Permissions.Where(p => p.RoleId == RoleID).ToList();
            var Functions = db.Functions.ToList();
            return View("ListByRoleID",Functions);
        }



        // GET: Admin/Authorization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Authorization/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Authorization/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Authorization/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Authorization/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Authorization/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Authorization/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
