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
        public KetNoi() { }
        public MongoCollection<BsonDocument> GetAllCollection(string nameCollection)
        {
            String connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase("LoyalCustomersManagement");
            MongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(nameCollection);
            return collection;
        }
    }
}
