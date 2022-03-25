using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using PagedList;
using Ictshop.App_Start;

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

        // Xem chi tiết người dùng GET: Admin/Home/Details/5 
        public ActionResult Details(int id)
        {
            var dt = db.Products.Find(id);
            return View(dt);
        }

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
                // Sửa sản phẩm theo mã sản phẩm
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

        // Xoá sản phẩm phương thức GET: Admin/Home/Delete/5
        public ActionResult Delete(int id)
        {
            var dt = db.Products.Find(id);
            return View(dt);
        }



        // Tạo sản phẩm mới phương thức POST: Admin/Home/Create
        [HttpPost]
        public ActionResult Create(Product Product)
        {
            try
            {
                Product.BrandID = 5;
                Product.CateID = 3;
                //Thêm  sản phẩm mới
                db.Products.Add(Product);
                // Lưu lại
                db.SaveChanges();
                // Thành công chuyển đến trang index
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }





        // Xoá sản phẩm phương thức POST: Admin/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //Lấy được thông tin sản phẩm theo ID(mã sản phẩm)
                var dt = db.Products.Find(id);
                // Xoá
                db.Products.Remove(dt);
                // Lưu lại
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
