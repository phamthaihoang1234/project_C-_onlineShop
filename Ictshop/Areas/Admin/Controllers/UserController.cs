using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using PagedList;

namespace Ictshop.Areas.Admin.Controllers
{
    [AdminAuthorize(FunctionCode = "PM6")]
    public class UsersController : Controller
    {
        private ShopManagement db = new ShopManagement();

        // Xem quản lý tất cả người dùng
        // GET: Admin/Users
        public ActionResult Index(int? page)
        {
            //Pageing
            if (page == null) page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);


            var Users = db.Users.OrderBy(n => n.RoleID);



            return View(Users.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            return View();
        }

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FullName,Email,Phone,Password,Address,RoleID")] User User)
        {
            //Lưu mât khẩu dưới dạng mã hóa
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(User.Password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPassWord = "";

            foreach (byte b in hasData)
            {
                hasPassWord += b;
            }
            User.Password = hasPassWord;
            //Check valid
            if (ModelState.IsValid)
            {
                db.Users.Add(User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", User.RoleID);
            return View(User);
        }


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
        public ActionResult Edit([Bind(Include = "UserID,FullName,Email,Phone,Password,Address,RoleID")] User User)
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
