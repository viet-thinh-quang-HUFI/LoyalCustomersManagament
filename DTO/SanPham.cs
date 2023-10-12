using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalCustomersManagament
{
    public class SanPham
    {
        private string maSP;
        private string tenSP;
        private Int32 donGia;
        private Int32 soLuongTon;
        private string hang;
        private Double kichThuoc;
        private string hieuNang;
        private Int32 trongLuong;
        public string MaSP { get => maSP; set => maSP = value; }
        public string TenSP { get => tenSP; set => tenSP = value; }
        public string Hang { get => hang; set => hang = value; }
        public int DonGia { get => donGia; set => donGia = value; }
        public int SoLuongTon { get => soLuongTon; set => soLuongTon = value; }
        public string HieuNang { get => hieuNang; set => hieuNang = value; }
        public int TrongLuong { get => trongLuong; set => trongLuong = value; }
        public double KichThuoc { get => kichThuoc; set => kichThuoc = value; }

        public SanPham() { }
        public SanPham(string ma, string ten, Int32 dongia, Int32 sl, string hangsp, Double kichthuoc, string hieunang, Int32 trongluong) 
        { 
            maSP = ma;  
            tenSP = ten;    
            DonGia = dongia;    
            SoLuongTon = sl;
            hang = hangsp;
            KichThuoc  = kichthuoc;
            hieuNang = hieunang;
            trongLuong = trongluong;
        }
    }
}
