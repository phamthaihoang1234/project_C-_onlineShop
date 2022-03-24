using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
namespace Ictshop.Controllers
{
    public class UserController : Controller
    {
        ShopManagement db = new ShopManagement();
        // ĐĂNG KÝ
        public ActionResult Dangky()
        {
            return View();
        }

        // ĐĂNG KÝ PHƯƠNG THỨC POST
        [HttpPost]
        public ActionResult Dangky([Bind(Include = "UserID,FullName,Email,Phone,Password,Address")] User User)
        {
            try
            {
                User.RoleID = 4;

                //Lưu mât khẩu dưới dạng mã hóa
                byte[] temp = ASCIIEncoding.ASCII.GetBytes(User.Password);
                byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

                string hasPassWord = "";

                foreach (byte b in hasData)
                {
                    hasPassWord += b;
                }
                User.Password = hasPassWord;
                // Thêm người dùng  mới
                db.Users.Add(User);
                // Lưu lại vào cơ sở dữ liệu
               
                db.SaveChanges();
                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Dangnhap");
                }
                return View("Dangky");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Dangnhap()
        {
            return View();

        }


        [HttpPost]
        public ActionResult Dangnhap(FormCollection userlog)
        {
            string userMail = userlog["userMail"].ToString();
            string password = userlog["password"].ToString();

            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPassWord = "";

            foreach (byte b in hasData)
            {
                hasPassWord += b;
            }


            var islogin = db.Users.SingleOrDefault(x => x.Email.Equals(userMail) && x.Password.Equals(hasPassWord));

            if (islogin != null)
            {
                if (userMail == "Admin@gmail.com")
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Admin/Home");
                }
                else
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Fail = "Đăng nhập thất bại";
                return View("Dangnhap");
            }

        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }


    }
}