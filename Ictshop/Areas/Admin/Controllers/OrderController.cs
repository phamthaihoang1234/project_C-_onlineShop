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
        public ActionResult Search(int? page, DateTime dateFrom,DateTime dateTo)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var orders = from item in db.Orders.ToList()
                         where item.OrderDate >= dateFrom && item.OrderDate <= dateTo   
                         select item;       
            var model = orders.ToList();
            ViewBag.Status = db.Order_Status.ToList();
            
            return View("Index", model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id)
        {
            var order = db.Orders.Find(id);    
            return View(order);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        public ActionResult Edit(int id)
        {
            var dt = db.Orders.Find(id);
            var hdhselected = new SelectList(db.Order_Status, "StatusID", "StatusName", dt.StatusID);
            ViewBag.StatusID = hdhselected;
            return View(dt);
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            try
            {
                var oldItem = db.Orders.Find(order.OrderID);
                oldItem.StatusID = order.StatusID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            
            return View();
        }

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
