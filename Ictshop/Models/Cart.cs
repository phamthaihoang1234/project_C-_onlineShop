using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ictshop.Models
{
    public class Cart
    {
        ShopManagement db = new ShopManagement();
        public int cProID { get; set; }
        public string cProName { get; set; }
        public string cProImage { get; set; }
        public double cProPrice { get; set; }
        public int cProQuantity { get; set; }
        public double ThanhTien
        {
            get { return cProQuantity * cProPrice; }
        }
        //Hàm tạo cho giỏ hàng
        public Cart(int ProductID)
        {
            cProID = ProductID;
            Product p = db.Products.Single(n => n.ProductID == cProID);
            cProName = p.ProductName;
            cProImage = p.Image;
            cProPrice = double.Parse(p.Price.ToString());
            cProQuantity = 1;
        }

    }
}