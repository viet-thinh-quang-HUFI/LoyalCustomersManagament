using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;

namespace DAL
{
    public class KetNoi
    {
        private static String connectionString = "mongodb://localhost:27017";
        private static String databaseName = "LoyalCustomersManagement";

        private IMongoClient _client;
        private IMongoDatabase _database;

        public IMongoClient Client { get => _client; set => _client = value; }
        public IMongoDatabase Database { get => _database; set => _database = value; }

        public KetNoi()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            //var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }

        //public IMongoCollection<BsonDocument> GetCollection(string nameCollection)
        //{
        //    IMongoCollection<BsonDocument> collection = Database.GetCollection<BsonDocument>(nameCollection);
        //    return collection;
        //}
    }
}
