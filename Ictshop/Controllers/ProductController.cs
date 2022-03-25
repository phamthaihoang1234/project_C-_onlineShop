using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using PagedList;

namespace Ictshop.Controllers
{
    public class ProductController : Controller
    {
        ShopManagement db = new ShopManagement();
        public ActionResult Monitorpartial()
        {
            var Monitor = db.Products.Where(n=>n.CateID==2).Take(4).ToList();
           return PartialView(Monitor);
        }
        public ActionResult Asuspartial()
        {
            var Asus = db.Products.Where(n => n.BrandID == 1).Take(4).ToList();
            return PartialView(Asus);
        }
        public ActionResult Dellpartial()
        {
            var Dell = db.Products.Where(n => n.BrandID == 2).Take(4).ToList();
            return PartialView(Dell);
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
        public ActionResult ListProduct(int? page, int BrandID = 0, int CateID = 0)
        {           
            if (page == null) page = 1;
            var model = db.Products.OrderBy(x => x.ProductID).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.BrandID = BrandID;
            ViewBag.CateID = CateID;
            if (BrandID != 0)
            {
                 model = db.Products.Where(p => p.BrandID == BrandID).ToList();
               
            }
            if (CateID != 0)
            {
                 model = db.Products.Where(p => p.CateID == CateID).ToList();
               
            }
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ListBrand()
        {
            var model = db.Brands.ToList();
            return PartialView(model);
        }
        public ActionResult ListByBrand(int? page,int BrandID = 0)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.BrandID = BrandID;  
            if (BrandID != 0)
            {
                var model = db.Products.Where(p => p.BrandID  == BrandID).ToList();
                return View("ListProduct", model.ToPagedList(pageNumber, pageSize));
            }
            return View();
        }

        public ActionResult ListByCateID(int? page,int CateID = 0)
        {
            if (page == null) page = 1;        
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.CateID = CateID;
            if (CateID != 0)
            {
                var model = db.Products.Where(p => p.CateID == CateID).ToList();
                return View("ListProduct", model.ToPagedList(pageNumber, pageSize));
            }
            return View();
        }

        public ActionResult Search(int? page, string key)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var products = db.Products.Where(p => p.ProductName.Contains(key));
            var model = products.OrderBy(x => x.ProductID).ToList();
            
            return View("ListProduct",model.ToPagedList(pageNumber, pageSize));
        }

    }

}