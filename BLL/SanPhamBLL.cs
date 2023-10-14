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
            //MoTa result = new MoTa();
            ////MongoCollection<BsonDocument> coll = SanPhamDAL.GetMoTa();

            //var builder = Builders<BsonDocument>.Filter;
            //var filter = builder.Eq("MaSP", maSP);

            //BsonDocument document = collection.Find(filter).FirstOrDefault();


            ////var sp = coll.Find((IMongoQuery)filter).FirstOrDefault();
            //try
            //{
            //    var moTa = document["Mota"];
            //    if (document != null)
            //    {
            //        result.HieuNang = moTa["Hieunang"].AsString;
            //        result.KichThuoc = moTa["Kichthuoc"].AsDouble;
            //        result.TrongLuong = moTa["Trongluong"].AsInt32;
            //        //Console.WriteLine(moTa.HieuNang.ToString());
            //    }
            //}
            //catch (Exception e)
            //{
                return null;
            //}
            //return result;
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
