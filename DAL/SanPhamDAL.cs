using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Runtime.Documents;
using MongoDB.Driver.Core.Configuration;

namespace DAL
{
    public class SanPhamDAL
    {
        KetNoi conn = new KetNoi();
        public SanPhamDAL() { }

        public MongoCollection<BsonDocument> GetMoTa()
        {
            MongoCollection<BsonDocument> collection = conn.Database.GetCollection<BsonDocument>("Mota");
            return collection;
        }

        public MongoCollection<BsonDocument> GetSanPham()
        {
            MongoCollection<BsonDocument> collection = conn.Database.GetCollection("SanPham");
            return collection;
        }

        public void Them(BsonDocument document)
        {
            MongoCollection<BsonDocument> collection = conn.Database.GetCollection("SanPham");
            collection.Insert(document);
        }
    }
}
