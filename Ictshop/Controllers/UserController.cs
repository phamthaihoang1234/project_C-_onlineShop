using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Ictshop.Models;
namespace Ictshop.Controllers
{
    public class UserController : Controller
    {
        ShopManagement db = new ShopManagement();
        // ĐĂNG KÝ
        public ActionResult Register()
        {
            var email = Session["email"];
            return View();
        }
        public ActionResult CheckDuplicate(string email)
        {
            if (db.Users.FirstOrDefault(u => u.Email.Equals(email)) != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Exist!');window.location.href = '/User/Login';</script>");
            }
            Session["email"] = email;
            return Content("<script language='javascript' type='text/javascript'>window.location.href = '/User/Register';</script>");

        }

        // ĐĂNG KÝ PHƯƠNG THỨC POST
        [HttpPost]
        public ActionResult Register([Bind(Include = "UserID,FullName,Email,Phone,Password,Address")] User User)
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
                Session["email"] = null;
                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Login");
                }
                return View("Register");

            }
            catch
            {
                return View();
            }


        }


        public ActionResult Login()
        {
            return View();

        }


        [HttpPost]
        public ActionResult Login(FormCollection userlog)
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
                return View("Login");
            }

        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }

        public string generatePassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        public ActionResult ForgotPass()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ForgotPass(string email)
        {

            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Credentials = new System.Net.NetworkCredential("ducanhbui09@gmail.com", "0943993221");
            smtpServer.Port = 587;
            smtpServer.EnableSsl = true;

            var newpass = generatePassword().Trim();

            mail.From = new MailAddress("ducanhbui09@gmail.com");
            mail.To.Add(email);
            mail.Subject = "CONFIRM YOUR ACCOUNT";
            mail.Body = "Email: " + email + "\nPassword: " + newpass;

            var user = db.Users.SingleOrDefault(u => u.Email == email);
            if (user != null)
            {
                byte[] temp = ASCIIEncoding.ASCII.GetBytes(newpass);
                byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

                string hasPassWord = "";

                foreach (byte b in hasData)
                {
                    hasPassWord += b;
                }
                user.Password = hasPassWord;
                db.SaveChanges();
            }

            smtpServer.Send(mail);


            return Content("<script language='javascript' type='text/javascript'>alert('New password has been sent in your email. Pleasd check!');window.location.href = '/User/Login';</script>");
        }


    }
}