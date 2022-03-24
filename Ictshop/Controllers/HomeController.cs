using Ictshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ictshop.Controllers
{
    //test commit
    
    // Hello faceafajfajksfjkashfjk
    public class HomeController : Controller
    {

        ShopManagement db = new ShopManagement();
        public ActionResult Index()
        {   
            ViewBag.NewProducts = db.Products.OrderByDescending(p => p.ProductID).Take(3).ToList();
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SlidePartial()
        {
            return PartialView();

        }


        public ActionResult Category()
        {
            var model = db.Categorys.ToList();  

            return PartialView(model);
        }
    }
}