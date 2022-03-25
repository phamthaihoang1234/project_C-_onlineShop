using Ictshop.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace Ictshop.Areas.Admin.Controllers
{

    public class HomeController : Controller

    {
        ShopManagement db = new ShopManagement();

        // GET: Admin/Home

        [AdminAuthorize(FunctionCode = "PM4")]
        public ActionResult Index(int? page, string key)
        {

            if (page == null) page = 1;
            ViewBag.branch = db.Brands.ToList();
            var sp = db.Products.OrderBy(x => x.ProductID);

            int pageSize = 6;

            int pageNumber = (page ?? 1);


            var products = db.Products.Where(p => p.ProductName.Contains(key));
            var model = products.OrderBy(x => x.ProductID);



            if (model.ToList().Count > 0)
            {
                sp = model;
            }




            return View(sp.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Details(int id)
        {
            var dt = db.Products.Find(id);
            return View(dt);
        }

        [HttpPost]
        public ActionResult Index(int? page, int BrandID)
        {

            ViewBag.branch = db.Brands.ToList();
            ViewBag.BrandID = BrandID;
            var products = db.Products.ToList();
            if (BrandID != 0)
            {
                products = (from item in products
                            where item.BrandID == BrandID
                            select item).ToList();
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        //[HttpPost]
        //public ActionResult searchByCate(int? page, int CateID)
        //{

        //    ViewBag.cate = db.Categorys.ToList();
        //    ViewBag.CateID = CateID;
        //    var products = db.Products.ToList();
        //    if (CateID != 0)
        //    {
        //        products = (from item in products
        //                    where item.CateID == CateID
        //                    select item).ToList();
        //    }
        //    int pageSize = 6;
        //    int pageNumber = (page ?? 1);
        //    return View();
        //}

        // Tạo sản phẩm mới phương thức GET: Admin/Home/Create
        public ActionResult Create()
        {
            //Để tạo dropdownList bên view
            var hangselected = new SelectList(db.Brands, "BrandID", "BrandName");
            ViewBag.BrandID = hangselected;
            var hdhselected = new SelectList(db.Categorys, "CateID", "CateName");
            ViewBag.CateID = hdhselected;
            return View();
        }


        // Sửa sản phẩm GET lấy ra ID sản phẩm: Admin/Home/Edit/5
        public ActionResult Edit(int id)
        {
            // Hiển thị dropdownlist
            var dt = db.Products.Find(id);
            var hangselected = new SelectList(db.Brands, "BrandID", "BrandName", dt.BrandID);
            ViewBag.BrandID = hangselected;
            var hdhselected = new SelectList(db.Categorys, "CateID", "CateName", dt.CateID);
            ViewBag.CateID = hdhselected;
            // 
            return View(dt);

        }

        // edit
        [HttpPost]
        public ActionResult Edit(Product Product)
        {
            try
            {
                var oldItem = db.Products.Find(Product.ProductID);
                oldItem.ProductName = Product.ProductName;
                oldItem.Price = Product.Price;
                oldItem.Quantity = Product.Quantity;
                oldItem.Description = Product.Description;
                oldItem.Image = Product.Image;
                oldItem.Memory = Product.Memory;
                oldItem.Ram = Product.Ram;
                oldItem.BrandID = Product.BrandID;
                oldItem.CateID = Product.CateID;
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
            var dt = db.Products.Find(id);
            return View(dt);
        }



        [HttpPost]
        public ActionResult Create(Product Product)
        {
            var hangselected = new SelectList(db.Brands, "BrandID", "BrandName");
            ViewBag.BrandID = hangselected;
            var hdhselected = new SelectList(db.Categorys, "CateID", "CateName");
            ViewBag.CateID = hdhselected;
            try
            {

                db.Products.Add(Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }





        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var dt = db.Products.Find(id);
                db.Products.Remove(dt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
