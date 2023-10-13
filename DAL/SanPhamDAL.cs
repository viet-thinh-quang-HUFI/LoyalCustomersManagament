using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Runtime.Documents;

namespace DAL
{
    public class SanPhamDAL
    {
        KetNoi KetNoi = new KetNoi();
        public SanPhamDAL() { }

        public MongoCollection<BsonDocument> getSanPham()
        {
            MongoCollection<BsonDocument> collection = KetNoi.GetAllCollection("SanPham");
            return collection;
        }
        public IMongoCollection<BsonDocument> getMoTa()
        {
            IMongoCollection<BsonDocument> collection = KetNoi.getMoTa("SanPham");
            return collection;
        }
        public void them(BsonDocument document)
        {
            MongoCollection<BsonDocument> collection = KetNoi.GetAllCollection("SanPham");
            collection.Insert(document);
        }
    }
}
