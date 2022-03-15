﻿using System;
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
        public List<Cart> LayGioHang()
        {
            List<Cart> lstGioHang = Session["GioHang"] as List<Cart>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<Cart>();
                Session["GioHang"] = lstGioHang;
            }
            
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int cProID, string strURL)
        {
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == cProID);
            if ( sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> lstGioHang = LayGioHang();
            Cart Product = lstGioHang.Find(n => n.cProID == cProID);
            if (Product == null)
            {
                Product = new Cart(cProID);
                lstGioHang.Add(Product);
                return Redirect(strURL);
            }
            else
            {
                Product.cProQuantity++;
                return Redirect(strURL);
            }
        }
        public ActionResult CapNhatGioHang(int cProID, FormCollection f)
        {
            Product sp = db.Products.SingleOrDefault(n => n.ProductID== cProID);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> lstGioHang = LayGioHang();
            Cart Product = lstGioHang.SingleOrDefault(n => n.cProID == cProID);
            if (Product != null)
            {
                Product.cProQuantity = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int cProID)
        {
            Product sp = db.Products.SingleOrDefault(n => n.ProductID== cProID);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> lstGioHang = LayGioHang();
            Cart Product = lstGioHang.SingleOrDefault(n => n.cProID == cProID);
            if (Product != null)
            {
                lstGioHang.RemoveAll(n => n.cProID == cProID);

            }
            if (lstGioHang.Count == 0)
            {
                Session["GioHang"] = null;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> lstGioHang = LayGioHang();
            double totalPrice = 0;
            foreach (var item in lstGioHang)
            {
                totalPrice += item.ThanhTien;
            }
            ViewBag.TotalPrice = totalPrice;
            return View(lstGioHang);
        }
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
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }

        [HttpPost]
        public ActionResult DatHang()
        {
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            Order ddh = new Order();
            User kh = (User)Session["use"];
            List<Cart> gh = LayGioHang();
            ddh.UserID = kh.UserID;
            ddh.OrderDate = DateTime.Now;
            ddh.StatusID = 1;
            Console.WriteLine(ddh);
            db.Orders.Add(ddh);
            db.SaveChanges();
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
            Session["GioHang"] = null;
            return RedirectToAction("Index", "Orders");
        }


    }
}