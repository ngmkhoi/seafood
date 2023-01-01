using seaFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seaFood.Controllers
{
    public class shoppingcartController : Controller
    {
        // GET: shoppingcart
        SeaFoodEntities database = new SeaFoodEntities();
        public List<Cart> ShoppingCart()
        {
            List<Cart> _shoppingcart = Session["shoppingcart"] as List<Cart>;
            if(_shoppingcart == null)
            {
                _shoppingcart = new List<Cart>();
                Session["shoppingcart"] = _shoppingcart;
            }
            return _shoppingcart;
        }
        public RedirectToRouteResult AddToCart(int MaSP)
        {
            List<Cart> _shoppingcart = ShoppingCart();
            Cart sanpham = _shoppingcart.FirstOrDefault(s => s.MaSanPham == MaSP);
            if(sanpham == null)
            {
                sanpham = new Cart(MaSP);
                _shoppingcart.Add(sanpham);
            }
            else
            {
                sanpham.SoLuong++;
            }
            return RedirectToAction("Details", "products", new { id = MaSP });
        }
        private int Total()
        {
            int total = 0;
            List<Cart> _shoppingcart = ShoppingCart();
            if (_shoppingcart != null)
                total = _shoppingcart.Sum(sp => sp.SoLuong);
            return total;
        }
        private double Total_Price()
        {
            double total_price = 0;
            List<Cart> _shoppingcart = ShoppingCart();
            if (_shoppingcart != null)
                total_price = _shoppingcart.Sum(sp => sp.ThanhTien());
            return total_price;
        }
        public ActionResult ShowCart()
        {
            List<Cart> _shoppingcart = ShoppingCart();
            if (_shoppingcart == null || _shoppingcart.Count == 0)
            {
                return RedirectToAction("Index","products");
            }
            ViewBag.total = Total();
            ViewBag.total_price = Total_Price();
            return View(_shoppingcart);
        }
        public ActionResult UpdateOrder(int MaSP, int _quan)
        {
            List<Cart> _shoppingcart = ShoppingCart();
            var _pro = _shoppingcart.FirstOrDefault(s => s.MaSanPham == MaSP);
            if (_pro != null)
            {
                _pro.SoLuong = _quan;
            }
            return RedirectToAction("ShowCart");
        }

        public ActionResult Delete(int MaSP)
        {
            List<Cart> _shoppingcart = ShoppingCart();
            var _pro = _shoppingcart.FirstOrDefault(s => s.MaSanPham == MaSP);
            if (_pro != null)
            {
                _shoppingcart.RemoveAll(s => s.MaSanPham == MaSP);
                return RedirectToAction("ShowCart");
            }
            if (_shoppingcart.Count == 0)
                return RedirectToAction("Index", "products");
            return RedirectToAction("ShowCart");
        }

        public ActionResult ShoppingCartPartial()
        {
            ViewBag.total = Total();
            ViewBag.total_price = Total_Price();
            return PartialView();
        }
        public ActionResult Order()
        {
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("SignIn", "users");
            List<Cart> _shoppingcart = ShoppingCart();
            if (_shoppingcart == null || _shoppingcart.Count == 0)
                return RedirectToAction("Index", "products");

            ViewBag.total = Total();
            ViewBag.total_price = Total_Price();
            return View(_shoppingcart);
        }
        public ActionResult AcceptOrder()
        {
            user user = Session["TaiKhoan"] as user;
            List<Cart> _shoppingcart = ShoppingCart();
            order DonHang = new order();
            DonHang.id = user.id;
            DonHang.name_order = user.name;
            DonHang.address_order = user.address;
            DonHang.phone_order = user.phone;
            DonHang.date_order = DateTime.Now;
            database.orders.Add(DonHang);
            database.SaveChanges();
            foreach (var item in _shoppingcart)
            {
                order_details chiTiet = new order_details();
                chiTiet.order_id = DonHang.order_id;
                chiTiet.product_id = item.MaSanPham;
                chiTiet.quantity_order = item.SoLuong;
                chiTiet.total_price = (int?)(item.DonGia * item.SoLuong);
                database.order_details.Add(chiTiet);
            }
            database.SaveChanges();

            Session["shoppingcart"] = null;
            return RedirectToAction("OrderSuccess");
        }
        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}
    
