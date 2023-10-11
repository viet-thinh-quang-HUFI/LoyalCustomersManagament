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
        private string moTa;
        public string MaSP { get => maSP; set => maSP = value; }
        public string TenSP { get => tenSP; set => tenSP = value; }
        public string Hang { get => hang; set => hang = value; }
        public int DonGia { get => donGia; set => donGia = value; }
        public int SoLuongTon { get => soLuongTon; set => soLuongTon = value; }

        public SanPham() { }
        public SanPham(string ma, string ten, Int32 dongia, Int32 sl, string hangsp) 
        { 
            maSP = ma;  
            tenSP = ten;    
            DonGia = dongia;    
            SoLuongTon = sl;
            hang = hangsp;
        }
    }
}
