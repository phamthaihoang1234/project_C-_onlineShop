using Ictshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ictshop.Controllers
{
    public class SearchController : Controller
    {

        ShopManagement db = new ShopManagement(); 
        // GET: Search
        public ActionResult SearchResult(string key)
        {
            var products = db.Products.Where(p => p.ProductName.Contains(key)); 
            ViewBag.Key = key;  
            return View(products.OrderBy(p => p.ProductName));
        }
    }
}