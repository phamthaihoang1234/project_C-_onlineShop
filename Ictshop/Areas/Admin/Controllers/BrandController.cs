using System;
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
    [AdminAuthorize(FunctionCode = "PM2")]
    public class BrandsController : Controller
    {
        private ShopManagement db = new ShopManagement();

        // GET: Admin/Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        // GET: Admin/Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand Brand = db.Brands.Find(id);
            if (Brand == null)
            {
                return HttpNotFound();
            }
            return View(Brand);
        }

        // GET: Admin/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want TotalCost bind TotalCost, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandID,BrandName")] Brand Brand)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(Brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Brand);
        }

        // GET: Admin/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand Brand = db.Brands.Find(id);
            if (Brand == null)
            {
                return HttpNotFound();
            }
            return View(Brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want TotalCost bind TotalCost, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandID,BrandName")] Brand Brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Brand);
        }

        // GET: Admin/Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand Brand = db.Brands.Find(id);
            if (Brand == null)
            {
                return HttpNotFound();
            }
            return View(Brand);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand Brand = db.Brands.Find(id);
            db.Brands.Remove(Brand);
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
