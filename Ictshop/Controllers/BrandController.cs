using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using Newtonsoft.Json;

namespace Ictshop.Controllers
{
    public class BrandController : Controller
    {
        ShopManagement db = new ShopManagement();
        public ActionResult BrandPartial()
        {
            var brands = db.Brands.ToList();
            return PartialView(brands);
        }

        /*public JsonResult GetByName(String name)
        {
            List<Brand> brands = db.Brands.ToList();
            if (!String.IsNullOrEmpty(name))
            {
                brands = brands.FindAll(b => b.BrandName.StartsWith(name.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            var json = JsonConvert.SerializeObject(brands);
            return Json(brands, JsonRequestBehavior.AllowGet);
        }*/
    }
}