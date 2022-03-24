using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using Newtonsoft.Json;

namespace Ictshop.Areas.Admin.Controllers
{
    [AdminAuthorize(FunctionCode = "PM2")]
    public class BrandsController : Controller
    {
        private ShopManagement db = new ShopManagement();

        // GET: Admin/Brands
        public ActionResult Index(string name)
        {
            var data = db.Brands.ToList();
            if (!String.IsNullOrEmpty(name)){
                ViewBag.searchName = name;
                data = data.FindAll(b => b.BrandName.Contains(name.Trim()));
            }
            
            return View(data.ToList());
        }


        public JsonResult SearchByName(string name)
        {
            var data = from b in db.Brands select b;
            var json = JsonConvert.SerializeObject(data.ToList());
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchById(int? id)
        {
            var data = db.Brands.Find(id);
            return Json(data, JsonRequestBehavior.AllowGet);
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
