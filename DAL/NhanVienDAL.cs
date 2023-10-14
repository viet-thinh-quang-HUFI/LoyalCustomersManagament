using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhanVienDAL
    {
        KetNoi conn = new KetNoi();
        private IMongoCollection<NhanVien> collection;
        public NhanVienDAL()
        {
            collection = conn.Database.GetCollection<NhanVien>("NhanVien");
        }

        public IMongoCollection<NhanVien> GetNhanVien()
        {
            return collection;
        }

        public void UpdatePasswordNhanVien(String maNV, String newPassword)
        {
            var filter = Builders<NhanVien>.Filter.Eq(nv => nv.MaNV, maNV);
            var update = Builders<NhanVien>.Update.Set(nv => nv.Matkhau, newPassword);
            collection.UpdateOne(filter, update);
        }
    }
}
