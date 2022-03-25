using Ictshop.Models;
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
    [AdminAuthorize(FunctionCode = "PM8")]
    public class AuthorizationController : Controller
    {
        ShopManagement db = new ShopManagement();

        public ActionResult ListRoles()
        {
            var model = db.Roles.ToList();
            return View(model);
        }
        public ActionResult ListByRoleID(int RoleID)
        {
            ViewBag.RoleID = RoleID;
            ViewBag.ListPermissonByRoleID = db.Permissions.Where(p => p.RoleId == RoleID).ToList();
            TempData["ListPermissonByRoleID"] = db.Permissions.Where(p => p.RoleId == RoleID).ToList();
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
            ViewBag.RoleID = RoleID;
            ViewBag.ListPermissonByRoleID = db.Permissions.Where(p => p.RoleId == RoleID).ToList();
            var Functions = db.Functions.ToList();
            return View("ListByRoleID", Functions);
        }



        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
