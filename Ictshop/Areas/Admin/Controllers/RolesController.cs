﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Areas.Admin.Controllers
{
    [AdminAuthorize(FunctionCode = "PM1")]
    public class RolesController : Controller
    {
        private ShopManagement db = new ShopManagement();

        // GET: Admin/Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Admin/Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role Role = db.Roles.Find(id);
            if (Role == null)
            {
                return HttpNotFound();
            }
            return View(Role);
        }

        // GET: Admin/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want TotalCost bind TotalCost, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleID,RoleName")] Role Role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(Role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Role);
        }

        // GET: Admin/Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role Role = db.Roles.Find(id);
            if (Role == null)
            {
                return HttpNotFound();
            }
            return View(Role);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want TotalCost bind TotalCost, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleID,RoleName")] Role Role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Role);
        }

        // GET: Admin/Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role Role = db.Roles.Find(id);
            if (Role == null)
            {
                return HttpNotFound();
            }
            return View(Role);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role Role = db.Roles.Find(id);
            db.Roles.Remove(Role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}