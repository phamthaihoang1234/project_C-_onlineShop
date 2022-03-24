using Ictshop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ictshop.Areas.Admin.Controllers
{
    [AdminAuthorize(FunctionCode = "PM5")]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        ShopManagement db = new ShopManagement();
        public ActionResult Index(string sortOrder,int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
            var orders = from o in db.Orders select o;
            ViewBag.Status = db.Order_Status.ToList();
            switch (sortOrder)
            {
                case "name_desc":
                    orders = orders.OrderByDescending(s => s.User.FullName);
                    break;
                case "Date":
                    orders = orders.OrderBy(s => s.OrderDate);
                    break;
                case "date_desc":
                    orders = orders.OrderByDescending(s => s.OrderDate);
                    break;
                case "status_desc":
                    orders = orders.OrderByDescending(s => s.OrderDate);
                    break;
                case "Status":
                    orders = orders.OrderBy(s => s.OrderDate);
                    break;
                default:
                    orders = orders.OrderBy(s => s.User.FullName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Index(int StatusID,int? page)
        {
            ViewBag.CurrentSort = StatusID;
            ViewBag.Status = db.Order_Status.ToList();
            ViewBag.StatusID = StatusID;
            var orders = db.Orders.ToList() ;
            if(StatusID != 0)
            {
                orders= (from item in orders
                         where item.StatusID == StatusID
                         select item).ToList();
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
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
