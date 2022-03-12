using Ictshop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ictshop.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        ShopManagement db = new ShopManagement();
        public ActionResult Index()
        {
            var orders = db.Orders.OrderBy(x => x.OrderID);
            return View(orders);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int id)
        {
            var order = db.Orders.Find(id);    
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int id)
        {
            // Hiển thị dropdownlist
            var dt = db.Orders.Find(id);
            var hdhselected = new SelectList(db.Order_Status, "StatusID", "StatusName", dt.StatusID);
            ViewBag.StatusID = hdhselected;
            // 
            return View(dt);
        }

        // POST: Admin/Order/Edit/5
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldItem = db.Orders.Find(order.OrderID);
                oldItem.OrderDate = order.OrderDate;
                oldItem.StatusID = order.StatusID;
                // lưu lại
                db.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View();
        }

        // POST: Admin/Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
