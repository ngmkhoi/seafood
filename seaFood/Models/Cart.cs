    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

namespace seaFood.Models
{
    public class Cart
    {
        SeaFoodEntities db = new SeaFoodEntities();
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string AnhSanPham { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        //Tính thành tiền = DongGia * SoLuong
        public double ThanhTien()
        {
            return SoLuong * DonGia;
        }
        public Cart(int MaSanPham)
        {
            this.MaSanPham = MaSanPham;
            var sanpham = db.products.Single(s => s.product_id == this.MaSanPham);
            this.TenSanPham = sanpham.product_name;
            this.AnhSanPham = sanpham.images;
            this.DonGia = double.Parse(sanpham.price.ToString());
            this.SoLuong = 1;
        }
    }
}
    
