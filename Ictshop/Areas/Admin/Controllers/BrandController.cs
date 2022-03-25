using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using PagedList;

namespace Ictshop.Areas.Admin.Controllers
{
    [AdminAuthorize(FunctionCode = "PM2")]
    public class BrandsController : Controller
    {
        private ShopManagement db = new ShopManagement();

        public bool ContainsIgnoredCase(string word, string search)
        {
            return word.ToLower().Contains(search.ToLower());
        }
        public ActionResult Index(string name, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var data = db.Brands.ToList();
            if (!String.IsNullOrEmpty(name))
            {
                ViewBag.textSearch = name;
                StringComparison comp = new StringComparison();
                data = data.FindAll(b => ContainsIgnoredCase(b.BrandName, name));
            }
            return View(data.ToPagedList(pageNumber, pageSize));
        }

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandID,BrandName")] Brand Brand, string img)
        {
            if (ModelState.IsValid)
            {
                Brand.BrandImg = img;
                db.Brands.Add(Brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Brand);
        }

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
        public ActionResult Edit([Bind(Include = "BrandID,BrandName")] Brand Brand, string img)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Brand);
        }

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
