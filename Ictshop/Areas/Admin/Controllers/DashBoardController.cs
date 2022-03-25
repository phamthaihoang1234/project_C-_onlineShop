using Ictshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ictshop.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: Admin/DashBoard
        ShopManagement context = new ShopManagement();
        public ActionResult Index()
        {
            ViewBag.Total = context.Orders.ToList().Count();
            ViewBag.Success = context.Orders.ToList().Count(s => s.StatusID == 2);
            ViewBag.Cancel = context.Orders.ToList().Count(c => c.StatusID == 3);
            ViewBag.New = context.Products.ToList().Count(n => n.NewProduct == true);
            return View();
        }
    }
}