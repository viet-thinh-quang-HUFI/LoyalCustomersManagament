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
        public IMongoCollection<BsonDocument> GetKhachHang()
        {
            IMongoCollection<BsonDocument> collection = KetNoi.Database.GetCollection<BsonDocument>("KhachHang");
            return collection;
        }
        public void Them(BsonDocument document)
        {
            IMongoCollection<BsonDocument> collection = KetNoi.Database.GetCollection<BsonDocument>("KhachHang");
            collection.InsertOne(document);
        }
    }
}
