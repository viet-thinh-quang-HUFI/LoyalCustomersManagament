using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KhachHang
    {
        private string maKH;
        private string hoTen;
        private int tuoi;
        private string sdt;
        private string email;
        private int diem;
        public KhachHang() { }

        public KhachHang(string maKH, string hoTen, int tuoi, string sdt, string email, int diem)
        {
            this.maKH = maKH;
            this.hoTen = hoTen;
            this.tuoi = tuoi;
            this.sdt = sdt;
            this.email = email;
            this.diem = diem;
        }

        public string MaKH { get => maKH; set => maKH = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public int Tuoi { get => tuoi; set => tuoi = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Email { get => email; set => email = value; }
        public int Diem { get => diem; set => diem = value; }
    }
}
