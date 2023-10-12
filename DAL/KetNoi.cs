using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DAL
{
    public class KetNoi
    {
        String connectionString = "mongodb://localhost:27017";
        public KetNoi() { }
        public MongoCollection<BsonDocument> GetAllCollection(string nameCollection)
        {
            
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase("LoyalCustomersManagement");
            MongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(nameCollection);
            return collection;
        }
        public IMongoCollection<BsonDocument> getMoTa(string nameCollection)
        {
            MongoClient client = new MongoClient(connectionString);
            var db = client.GetDatabase("LoyalCustomersManagement");
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(nameCollection);
            return collection;
        }
    }
}
