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
        private static String connectionString = "mongodb://localhost:27017";
        private static String databaseName = "LoyalCustomersManagement";

        private MongoClient _client;
        private MongoServer _server;
        private MongoDatabase _database;

        public MongoClient Client { get => _client; set => _client = value; }
        public MongoServer Server { get => _server; set => _server = value; }
        public MongoDatabase Database { get => _database; set => _database = value; }

        public KetNoi()
        {
            Client = new MongoClient(connectionString);
            Server = Client.GetServer();
            Database = Server.GetDatabase(databaseName);
        }

        public MongoCollection<BsonDocument> GetCollection(string nameCollection)
        {
            MongoCollection<BsonDocument> collection = Database.GetCollection<BsonDocument>(nameCollection);
            return collection;
        }
    }
}
