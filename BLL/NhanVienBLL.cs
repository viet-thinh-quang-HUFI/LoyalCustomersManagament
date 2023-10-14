using DAL;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace BLL
{
    public class NhanVienBLL
    {
        NhanVienDAL nhanVienDAL = new NhanVienDAL();
        public NhanVienBLL() { }

        public Boolean Login(NhanVien nhanVien)
        {
            if(nhanVien.EmailNV != String.Empty && nhanVien.Matkhau != String.Empty)
            {
                MongoCollection<BsonDocument> collection = nhanVienDAL.GetNhanVien();
                foreach (BsonDocument document in collection.FindAll())
                {
                    if (document["EmailNV"] == nhanVien.EmailNV && document["Matkhau"] == nhanVien.Matkhau)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
