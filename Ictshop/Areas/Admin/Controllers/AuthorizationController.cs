using Ictshop.Models;
using System;
using System.Collections.Generic;
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
            TempData["RoleID"] = RoleID;
            TempData["FunctionCode"] = db.Functions.ToList();
            TempData["ListPermissonByRoleID"] = db.Permissions.Where(p => p.RoleId == RoleID) .ToList();
            var Functions = db.Functions.ToList();
            return View(Functions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListByRoleID()
        {
            int RoleID = Int32.Parse(Request.Form["RoleID"]);
            db.Permissions.RemoveRange(db.Permissions.Where(p => p.RoleId == RoleID));
            db.SaveChanges();
            Permission permission = new Permission();
            var permissonsList = db.Permissions.ToList();
            string[] permissons = {"PM1", "PM2"};
            foreach (string item in permissons)
            {
                permission.RoleId = RoleID;
                permission.FunctionCode = item;
                permissonsList.Add(permission);
              
            }
            db.Permissions.AddRange(permissonsList);
            db.SaveChanges();
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
