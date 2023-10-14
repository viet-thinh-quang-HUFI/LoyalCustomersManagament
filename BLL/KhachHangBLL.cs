using DAL;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class KhachHangBLL
    {
        KhachHangDAL khachHangDAL = new KhachHangDAL();
        IMongoCollection<BsonDocument> collection;
        public KhachHangBLL() {
            collection = khachHangDAL.GetKhachHang();
        }
        public List<KhachHang> GetKhachHang()
        {
            List<KhachHang> KhachHangs = new List<KhachHang>();
            foreach (BsonDocument document in collection.Find(new BsonDocument()).ToList())
            {
                KhachHang KhachHang = new KhachHang();
                KhachHang.MaKH = document["MaKH"].AsString;
                KhachHang.HoTen = document["Hoten"].AsString;
                KhachHang.Tuoi = document["Tuoi"].AsInt32;
                KhachHang.Sdt = document["SDT"].AsString;
                KhachHang.Email = document["EmailKH"].AsString;
                KhachHang.Diem = document["Diem"].AsInt32;

                KhachHangs.Add(KhachHang);
            }
            return KhachHangs;
        }
        public string Them(string ma, string ten, string tuoi, string sdt, string mail, string diem)
        {
            string kq = "";
            if (ma == "")
            {
                return "Chưa nhập mã! ";
            }
            if (ten == "")
            {
                return "Chưa nhập họ tên! ";
            }
            if (tuoi == "")
            {
                return "Chưa nhập tuổi! ";
            }
            if (sdt == "")
            {
                return "Chưa nhập số điện thoại! ";
            }
            if (mail == "")
            {
                return "Chưa nhập email! ";
            }
            if (diem == "")
            {
                return "Chưa nhập diem! ";
            }
            if (IsNumber(tuoi) == false || Convert.ToInt32(tuoi) < 0)
            {
                return "Nhập tuổi sai";
            }
            if (IsNumber(diem) == false)
            {
                return "Nhập điểm sai";
            }
            if (IsValidEmail(mail) == false)
            {
                return "Nhập mail sai";
            }
            KhachHang s = new KhachHang(ma, ten, Convert.ToInt32(tuoi), sdt, mail, Convert.ToInt32(diem));
            BsonDocument document = new BsonDocument();
            document.Add("MaKH", s.MaKH);
            document.Add("Hoten", s.HoTen);
            document.Add("Tuoi", s.Tuoi);
            document.Add("SDT", s.Sdt);
            document.Add("EmailKH", s.Email);
            document.Add("Diem", s.Diem);
            khachHangDAL.Them(document);
            return "Thêm thành công";
        }
        public string xoa(string ma)
        {
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("MaKH", ma);
            collection.DeleteOne(deleteFilter);
            return "Xóa thành công";
        }
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}
