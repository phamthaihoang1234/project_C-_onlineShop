using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Controllers
{
    public class CartController : Controller
    {
        ShopManagement db = new ShopManagement();
        // GET: GioHang
        
        //Lấy giỏ hàng 
        public List<Cart> LayGioHang()
        {
            List<Cart> lstGioHang = Session["GioHang"] as List<Cart>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<Cart>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int cProID, string strURL)
        {
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == cProID);
            if ( sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<Cart> lstGioHang = LayGioHang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            Cart Product = lstGioHang.Find(n => n.cProID == cProID);
            if (Product == null)
            {
                Product = new Cart(cProID);
                //Add sản phẩm mới thêm vào list
                lstGioHang.Add(Product);
                return Redirect(strURL);
            }
            else
            {
                Product.cProQuantity++;
                return Redirect(strURL);
            }
        }
        //Cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(int cProID, FormCollection f)
        {
            //Kiểm tra masp
            Product sp = db.Products.SingleOrDefault(n => n.ProductID== cProID);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<Cart> lstGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            Cart Product = lstGioHang.SingleOrDefault(n => n.cProID == cProID);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (Product != null)
            {
                Product.cProQuantity = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int cProID)
        {
            //Kiểm tra masp
            Product sp = db.Products.SingleOrDefault(n => n.ProductID== cProID);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<Cart> lstGioHang = LayGioHang();
            Cart Product = lstGioHang.SingleOrDefault(n => n.cProID == cProID);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (Product != null)
            {
                lstGioHang.RemoveAll(n => n.cProID == cProID);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Cart> lstGioHang = Session["GioHang"] as List<Cart>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.cProQuantity);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<Cart> lstGioHang = Session["GioHang"] as List<Cart>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        //tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }

        #region // Mới hoàn thiện
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            Order ddh = new Order();
            User kh = (User)Session["use"];
            List<Cart> gh = LayGioHang();
            ddh.UserID = kh.UserID;
            ddh.OrderDate = DateTime.Now;
            ddh.StatusID = 1;
            Console.WriteLine(ddh);
            db.Orders.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                OrderDetail ctDH = new OrderDetail();
                decimal thanhtien =  item.cProQuantity * (decimal) item.cProPrice;
                ctDH.OrderID = ddh.OrderID;
                ctDH.ProductID = item.cProID;
                ctDH.Quantity = item.cProQuantity;
                ctDH.UnitPrice = (decimal)item.cProPrice;
                ctDH.TotalCost =  thanhtien;
                db.OrderDetails.Add(ctDH);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Orders");
        }
        #endregion

    }
}