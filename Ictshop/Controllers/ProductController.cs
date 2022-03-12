using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Controllers
{
    public class ProductController : Controller
    {
        ShopManagement db = new ShopManagement();
        public ActionResult dtiphonepartial()
        {
            var ip = db.Products.Where(n=>n.ProductID==2).Take(4).ToList();
           return PartialView(ip);
        }
        public ActionResult dtsamsungpartial()
        {
            var ss = db.Products.Where(n => n.ProductID == 1).Take(4).ToList();
            return PartialView(ss);
        }
        public ActionResult dtxiaomipartial()
        {
            var mi = db.Products.Where(n => n.ProductID == 3).Take(4).ToList();
            return PartialView(mi);
        }
        public ActionResult xemchitiet(int ProductID=0)
        {
            var chitiet = db.Products.SingleOrDefault(n=>n.ProductID== ProductID);
            if (chitiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chitiet);
        }
        public ActionResult ListProduct()
        {
            var model = db.Products.ToList();
            return View(model);
        }

        public ActionResult ListBrand()
        {
            var model = db.Brands.ToList();
            return PartialView(model);
        }
        public ActionResult ListByBrand(int BrandID = 0)
        {
            if(BrandID != 0)
            {
                var model = db.Products.Where(p => p.BrandID  == BrandID).ToList();
                return View("ListProduct",model);
            }
            return View();
        }

        public ActionResult ListByCateID(int CateID = 0)
        {
            if (CateID != 0)
            {
                var model = db.Products.Where(p => p.CateID == CateID).ToList();
                return View("ListProduct", model);
            }
            return View();
        }

    }

}