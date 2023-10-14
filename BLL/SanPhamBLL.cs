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
        IMongoCollection<BsonDocument> collection;

        public SanPhamBLL()
        {
            collection = sanPhamDAL.GetSanPham();
        }

        public List<SanPham> GetSanPham()
        {
            List<SanPham> sanPhams = new List<SanPham>();
            foreach (BsonDocument document in collection.Find(new BsonDocument()).ToList())
            {
                SanPham sanPham = new SanPham();
                sanPham.MaSP = document["MaSP"].AsString;
                sanPham.TenSP = document["TenSP"].AsString;
                sanPham.DonGia = document["Dongia"].AsInt32;
                sanPham.SoLuongTon = document["Soluongton"].AsInt32;
                sanPham.Hang = document["Mahang"].AsString;

                //BsonDocument Mota = document["Mota"].ToBsonDocument();
                //sanPham.KichThuoc = Mota["Kichthuong"].AsDouble;
                //sanPham.HieuNang = Mota["Hieunang"].AsString;
                //sanPham.TrongLuong = Mota["Trongluong"].AsInt32;

                sanPhams.Add(sanPham);
            }
            return sanPhams;
        }
        public MoTa GetMoTa(string maSP)
        {
            MoTa result = new MoTa();
            //MongoCollection<BsonDocument> coll = SanPhamDAL.GetMoTa();

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("MaSP", maSP);

            BsonDocument document = collection.Find(filter).FirstOrDefault();


            //var sp = coll.Find((IMongoQuery)filter).FirstOrDefault();
            try
            {
                var moTa = document["Mota"];

                if (document != null)
                {
                    result.HieuNang = moTa["Hieunang"].AsString;
                    result.KichThuoc = moTa["Kichthuoc"].AsDouble;
                    result.TrongLuong = moTa["Trongluong"].AsInt32;
                    //Console.WriteLine(moTa.HieuNang.ToString());
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return result;
        }
        //public void Them(SanPham s)
        //{
        //    BsonDocument document = new BsonDocument();
        //    document.Add("MaSP", s.MaSP);
        //    document.Add("TenSP", s.TenSP);
        //    document.Add("Dongia", s.DonGia);
        //    document.Add("Soluongton", s.SoLuongTon);
        //    document.Add("Mahang", s.Hang);
        //    SanPhamDAL.Them(document);
        //}
    }
}
