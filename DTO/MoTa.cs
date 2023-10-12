using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MoTa
    {
        public MoTa() { }
        private Double kichThuoc;
        private string hieuNang;
        private Int32 trongLuong;
        public string HieuNang { get => hieuNang; set => hieuNang = value; }
        public int TrongLuong { get => trongLuong; set => trongLuong = value; }
        public double KichThuoc { get => kichThuoc; set => kichThuoc = value; }
        public MoTa(Double kichthuoc, string hieunang, Int32 trongluong)
        {
            HieuNang = hieunang;
            TrongLuong = trongluong;
            KichThuoc= kichthuoc;
        }
    }
}
