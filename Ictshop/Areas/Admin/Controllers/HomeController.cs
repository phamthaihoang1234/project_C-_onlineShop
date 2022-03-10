using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using PagedList;

namespace Ictshop.Areas.Admin.Controllers
{
    public class HomeController : Controller
        
    {
        Qlbanhang db = new Qlbanhang();
       
        // GET: Admin/Home
        
        public ActionResult Index(int ?page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.Products.OrderBy(x => x.Masp);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(sp.ToPagedList(pageNumber, pageSize));

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
            var hangselected = new SelectList(db.Brands, "Mahang", "Tenhang");
            ViewBag.Mahang = hangselected;
            var hdhselected = new SelectList(db.Categorys, "Mahdh", "Tenhdh");
            ViewBag.Mahdh = hdhselected;
            return View();
        }

        // Tạo sản phẩm mới phương thức POST: Admin/Home/Create
        [HttpPost]
        public ActionResult Create(Product Product)
        {
            try
            { 
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

        // Sửa sản phẩm GET lấy ra ID sản phẩm: Admin/Home/Edit/5
        public ActionResult Edit(int id)
        {
            // Hiển thị dropdownlist
            var dt = db.Products.Find(id);
            var hangselected = new SelectList(db.Brands, "Mahang", "Tenhang",dt.Mahang);
            ViewBag.Mahang = hangselected;
            var hdhselected = new SelectList(db.Categorys, "Mahdh", "Tenhdh",dt.Mahdh);
            ViewBag.Mahdh = hdhselected;
           // 
            return View(dt);
            
        }

        // POST: Admin/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Product Product)
        {
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldItem = db.Products.Find(Product.Masp);
                oldItem.Tensp = Product.Tensp;
                oldItem.Giatien = Product.Giatien;
                oldItem.Soluong = Product.Soluong;
                oldItem.Mota = Product.Mota;
                oldItem.Anhbia = Product.Anhbia;
                oldItem.Bonhotrong = Product.Bonhotrong;
                oldItem.Ram = Product.Ram;
                oldItem.Thesim = Product.Thesim;
                oldItem.Mahang = Product.Mahang;
                oldItem.Mahdh = Product.Mahdh;
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
