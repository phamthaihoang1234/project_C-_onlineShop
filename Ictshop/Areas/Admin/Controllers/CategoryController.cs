using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using PagedList;

namespace Ictshop.Areas.Admin.Controllers
{
    [AdminAuthorize(FunctionCode = "PM3")]
    public class CategorysController : Controller
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

            var data = db.Categorys.ToList();
            if (!String.IsNullOrEmpty(name))
            {
                ViewBag.textSearch = name;
                data = data.FindAll(c => ContainsIgnoredCase(c.CateName, name));
            }
            return View(data.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = db.Categorys.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CateID,CateName")] Category Category)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Add(Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = db.Categorys.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CateID,CateName")] Category Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = db.Categorys.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category Category = db.Categorys.Find(id);
            db.Categorys.Remove(Category);
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
