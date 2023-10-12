using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BLL
{
    public class SanPhamBLL
    {
        SanPhamDAL SanPhamDAL = new SanPhamDAL();
        public SanPhamBLL() { }
        public List<SanPham>  getSanPham()
        {
            List<SanPham> sanPhams = new List<SanPham>();
            MongoCollection<BsonDocument> collection = SanPhamDAL.getSanPham();
            foreach (BsonDocument document in collection.FindAll())
            {
                SanPham sanPham = new SanPham();
                sanPham.MaSP = document["MaSP"].AsString;
                sanPham.TenSP = document["TenSP"].AsString;
                sanPham.DonGia = document["Dongia"].AsInt32;
                sanPham.SoLuongTon = document["Soluongton"].AsInt32;
                sanPham.Hang = document["Hang"].AsString;

                BsonDocument Mota = document["Mota"].ToBsonDocument();
                sanPham.KichThuoc = Mota["Kichthuong"].AsDouble;
                sanPham.HieuNang = Mota["Hieunang"].AsString;
                sanPham.TrongLuong = Mota["Trongluong"].AsInt32;

                sanPhams.Add(sanPham);
            }
            return sanPhams;
        }
    }
}
