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
        // GET: Admin/Categorys
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

        // GET: Admin/Categorys/Details/5
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

        // GET: Admin/Categorys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categorys/Create
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

        // GET: Admin/Categorys/Edit/5
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

        // POST: Admin/Categorys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want TotalCost bind TotalCost, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Admin/Categorys/Delete/5
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

        // POST: Admin/Categorys/Delete/5
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
