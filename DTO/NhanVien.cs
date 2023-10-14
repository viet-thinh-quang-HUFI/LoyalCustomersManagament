using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhanVien
    {
        private String _MaNV;
        private String _HotenNV;
        private String _EmailNV;
        private String _Matkhau;
        private Int16 _KPI;
        private List<String> _MaKH;

        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public string HotenNV { get => _HotenNV; set => _HotenNV = value; }
        public string EmailNV { get => _EmailNV; set => _EmailNV = value; }
        public string Matkhau { get => _Matkhau; set => _Matkhau = value; }
        public short KPI { get => _KPI; set => _KPI = value; }
        public List<string> MaKH { get => _MaKH; set => _MaKH = value; }

        public NhanVien()
        {
            MaNV = "";
            HotenNV = "";
            EmailNV = "";
            Matkhau = "";
            KPI = 0;
            MaKH = new List<String>();
        }

        public NhanVien(string maNV, string hotenNV, string emailNV, string matkhau, short kPI, List<string> maKH)
        {
            MaNV = maNV;
            HotenNV = hotenNV;
            EmailNV = emailNV;
            Matkhau = matkhau;
            KPI = kPI;
            MaKH = maKH;
        }
    }
}
