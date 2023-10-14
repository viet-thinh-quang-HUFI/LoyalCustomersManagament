using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class KhachHangDAL
    {
        KetNoi KetNoi = new KetNoi();
        public KhachHangDAL() { }
        public MongoCollection<BsonDocument> getKhachHang()
        {
            MongoCollection<BsonDocument> collection = KetNoi.Database.GetCollection("KhachHang");
            return collection;
        }
    }
}
