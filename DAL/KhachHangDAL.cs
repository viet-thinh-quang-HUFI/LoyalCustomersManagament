using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class KhachHangDAL
    {
        KetNoi conn = new KetNoi();
        private IMongoCollection<KhachHang> collection;
        public KhachHangDAL() 
        {
            collection = conn.Database.GetCollection<KhachHang>("KhachHang");
        }
        public IMongoCollection<KhachHang> GetKhachHang()
        {
            return collection;
        }

        public void Them(KhachHang kh)
        {
            collection.InsertOne(kh);
        }

    }
}
