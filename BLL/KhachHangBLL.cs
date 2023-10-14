using DAL;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class KhachHangBLL
    {
        //KhachHangDAL KhachHangDAL = new KhachHangDAL();
        //public KhachHangBLL() { }
        //public List<KhachHang> getKhachHang()
        //{
        //    List<KhachHang> KhachHangs = new List<KhachHang>();
        //    MongoCollection<BsonDocument> collection = KhachHangDAL.getKhachHang();
        //    foreach (BsonDocument document in collection.FindAll())
        //    {
        //        KhachHang KhachHang = new KhachHang();
        //        KhachHang.MaKH = document["MaKH"].AsString;
        //        KhachHang.HoTen = document["Hoten"].AsString;
        //        KhachHang.Tuoi = document["Tuoi"].AsInt32;
        //        KhachHang.Sdt = document["SDT"].AsString;
        //        KhachHang.Email = document["EmailKH"].AsString;
        //        KhachHang.Diem = document["Diem"].AsInt32;

        //        KhachHangs.Add(KhachHang);
        //    }
        //    return KhachHangs;
        //}
    }
}
