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
    [AdminAuthorize]
    public class UsersController : Controller
    {
        private ShopManagement db = new ShopManagement();

        // Xem quản lý tất cả người dùng
        // GET: Admin/Users
        public ActionResult Index()
        {
            var Users = db.Users.Include(n => n.Role);
            return View(Users.ToList());
        }

        //Xem chi tiết người dùng theo Mã người dùng
        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            // Nếu không có người dùng có mã được truyền vào thì trả về trang báo lỗi
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Khai báo một người dùng theo mã
            User User = db.Users.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            // trả về trang chi tiết người dùng
            return View(User);
        }

        //// GET: Admin/Users/Create
        //public ActionResult Create()
        //{
        //    ViewBag.IDQuyen = new SelectList(db.Roles, "IDQuyen", "TenQuyen");
        //    return View();
        //}

        //// POST: Admin/Users/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaUser,FullName,Email,Phone,Password,IDQuyen")] User User)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(User);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IDQuyen = new SelectList(db.Roles, "IDQuyen", "TenQuyen", User.IDQuyen);
        //    return View(User);
        //}


            // Chỉnh sửa người dùng
        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = db.Users.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", User.RoleID);
            return View(User);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FullName,Email,Phone,Password,RoleID")] User User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDQuyen = new SelectList(db.Roles, "RoleID", "RoleName", User.RoleID);
            return View(User);
        }

        // Xoá người dùng 
        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = db.Users.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            return View(User);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User User = db.Users.Find(id);
            db.Users.Remove(User);
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
