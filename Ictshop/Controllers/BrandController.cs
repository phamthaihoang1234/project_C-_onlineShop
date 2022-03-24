using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Controllers
{
    public class BrandController : Controller
    {
        ShopManagement db = new ShopManagement();
        public ActionResult BrandPartial()
        {
            var danhmuc = db.Brands.ToList();
            return PartialView(danhmuc);
        }
    }
}