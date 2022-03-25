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
    [AdminAuthorize(FunctionCode = "PM5")]
    public class UsersController : Controller
    {
        private ShopManagement db = new ShopManagement();

        public bool ContainsIgnoredCase(string word, string search)
        {
            return word.Trim().ToLower().Contains(search.Trim().ToLower());
        }
        public ActionResult Index(int? page, string name, string email, string phone, int? roleID)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            roleID = roleID ?? 0;

            ViewBag.txtName = name;
            ViewBag.txtEmail = email;
            ViewBag.txtPhone = phone;
            ViewBag.selectedID = roleID;
            var roles = db.Roles.ToList();
            ViewBag.Role = roles;
            ViewBag.SelectedRole = roleID;


            /*var users = db.Users.OrderBy(u => u.RoleID)
                .Where(u => String.IsNullOrEmpty(name) || ContainsIgnoredCase(u.FullName, name))
                .Where(u => String.IsNullOrEmpty(email) || ContainsIgnoredCase(u.Email, email))
                .Where(u => String.IsNullOrEmpty(phone) || ContainsIgnoredCase(u.Phone, phone))
                .Where(u => roleID == 0 || u.Role.RoleID == roleID);*/


            var users = db.Users.ToList()
                .FindAll(u => (String.IsNullOrEmpty(name) || ContainsIgnoredCase(u.FullName, name))
                        && (String.IsNullOrEmpty(email) || ContainsIgnoredCase(u.Email, email))
                        && (String.IsNullOrEmpty(phone) || ContainsIgnoredCase(u.Phone, phone))
                        && (roleID == 0 || u.Role.RoleID == roleID)
                       );

            return View(users.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Details(int? id)
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


        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FullName,Email,Phone,Password,Address,RoleID")] User User)
        {

            byte[] temp = ASCIIEncoding.ASCII.GetBytes(User.Password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPassWord = "";

            foreach (byte b in hasData)
            {
                hasPassWord += b;
            }
            User.Password = hasPassWord;

            if (ModelState.IsValid)
            {
                db.Users.Add(User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", User.RoleID);
            return View(User);
        }



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
