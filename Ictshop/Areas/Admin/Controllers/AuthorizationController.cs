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
        public ActionResult Home()
        {
            TempData["ListRoles"] = db.Roles.ToList();
            var Functions = db.Functions.ToList();
            return View(Functions);
        }

        public ActionResult ListRoles()
        {
            var model = db.Roles.ToList();
            return View(model);
        }
        public ActionResult ListByRoleID(int RoleID = 0)
        {
            TempData["ListRolesByID"] = db.Functions.SqlQuery("select * from [Function] f join " +
            "permission p on f.FunctionCode = p.FunctionCode where p.RoleId = @id", new SqlParameter("@id", RoleID));
            var Functions = db.Functions.ToList();
            return View("Home", Functions);
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
