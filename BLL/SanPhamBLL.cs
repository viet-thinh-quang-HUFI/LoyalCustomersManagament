using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BLL
{
    public class SanPhamBLL
    {
        SanPhamDAL sanPhamDAL = new SanPhamDAL();
        IMongoCollection<SanPham> collection;

        public SanPhamBLL()
        {
            collection = sanPhamDAL.GetSanPham();
        }

        public List<SanPham> GetSanPham()
        {
            var filter = Builders<SanPham>.Filter.Empty;
            var sanPhams = sanPhamDAL.GetSanPham()
                .Find(filter)
                .ToList();
            return sanPhams;
        }
        public MoTa GetMoTa(string maSP)
        {
            MoTa moTa = new MoTa();
            //MongoCollection<BsonDocument> coll = SanPhamDAL.GetMoTa();

            var filter = Builders<SanPham>.Filter.Eq(a => a.MaSP, maSP);

            try
            {
                moTa = collection.Find(filter).SingleOrDefault().MoTa;
            }
            catch (Exception e)
            {
                return null;
            }
            return moTa;
        }
        public void Them(SanPham s)
        {
            //BsonDocument document = new BsonDocument();
            //document.Add("MaSP", s.MaSP);
            //document.Add("TenSP", s.TenSP);
            //document.Add("Dongia", s.DonGia);
            //document.Add("Soluongton", s.SoLuongTon);
            //document.Add("Mahang", s.Hang);
            //sanPhamDAL.Them(document);
        }
    }
}
