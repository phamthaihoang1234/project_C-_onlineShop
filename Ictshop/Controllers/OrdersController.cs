using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Controllers
{
    public class OrdersController : Controller
    {
        private ShopManagement db = new ShopManagement();

        // GET: Orders
        // Hiển thị danh sách đơn hàng
        public ActionResult Index()
        {

            //Kiểm tra đang đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            User kh = (User)Session["use"];
            int maND = kh.UserID;
            var Orders = db.Orders.Include(d => d.User).Where(d => d.UserID == maND);
            return View(Orders.ToList());
        }

        // GET: Orders/Details/5
        //Hiển thị chi tiết đơn hàng
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = db.Orders.Find(id);
            var chitiet = db.OrderDetails.Include(d => d.Product).Where(d => d.OrderID == id).ToList();
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
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
