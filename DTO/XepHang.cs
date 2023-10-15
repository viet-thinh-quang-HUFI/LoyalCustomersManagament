using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class XepHang
    {
        private string _ten;
        private string _diemXepHang;
        
        public string Ten { get => _ten; set => _ten = value; }
        public string DiemXepHang { get => _diemXepHang; set => _diemXepHang = value; }

        public XepHang() { }
    }
}
