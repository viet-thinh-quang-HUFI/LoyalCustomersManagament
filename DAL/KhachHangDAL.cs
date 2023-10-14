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
        public IMongoCollection<BsonDocument> getKhachHang()
        {
            IMongoCollection<BsonDocument> collection = KetNoi.Database.GetCollection<BsonDocument>("KhachHang");
            return collection;
        }
    }
}
