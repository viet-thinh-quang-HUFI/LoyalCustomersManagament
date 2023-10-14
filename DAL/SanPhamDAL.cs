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

        public IMongoCollection<BsonDocument> GetMoTa()
        {
            IMongoCollection<BsonDocument> collection = conn.Database.GetCollection<BsonDocument>("Mota");
            return collection;
        }

        public IMongoCollection<BsonDocument> GetSanPham()
        {
            IMongoCollection<BsonDocument> collection = conn.Database.GetCollection<BsonDocument>("SanPham");
            return collection;
        }

        public void Them(BsonDocument document)
        {
            IMongoCollection<BsonDocument> collection = conn.Database.GetCollection<BsonDocument>("SanPham");
            collection.InsertOne(document);
        }
    }
}
