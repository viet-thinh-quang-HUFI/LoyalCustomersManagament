using DAL;
using DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HeThongBLL
    {
        HeThongDAL heThongDAL = new HeThongDAL();
        IMongoDatabase database;

        public HeThongBLL()
        {
            database = heThongDAL.GetDatabase();
        }

        public List<String> GetCollectionName()
        {
            List<String> rs = new List<String>();
            foreach (BsonDocument item in heThongDAL.GetDatabase()
                .ListCollectionsAsync().Result
                .ToListAsync<BsonDocument>().Result)
            {
                rs.Add(item["name"].ToString());
            }
            return rs;
        }

        public Double GetDatabaseSize()
        {
            var command = new CommandDocument { { "dbStats", 1 }, { "scale", 1024 } };
            var result = database.RunCommand<BsonDocument>(command);
            return result["dataSize"].AsDouble;
        }

        public Dictionary<String, Object> GetCollectionSizeAndCount(String collectionName)
        {
            Dictionary<String, Object> result = new Dictionary<String, Object>();
            var command = new CommandDocument { { "collstats", collectionName }, { "scale", 1 } };
            var document = database.RunCommand<BsonDocument>(command);

            Double size = Math.Round((Double)document["size"].AsInt32 / 1024, 1);
            Int32 num = document["count"].AsInt32;

            result.Add("size", size);
            result.Add("count", num);

            return result;
        }

        public Double GetNumberDocOfCollection(String collectionName)
        {
            var command = new CommandDocument { { "collstats", collectionName }, { "scale", 1 } };
            var result = database.RunCommand<BsonDocument>(command);
            return Math.Round((Double)result["size"].AsInt32 / 1024, 1);
        }

        public DataTable GetCollectionDetails(String collectionName)
        {
            var command = new CommandDocument { { "collstats", collectionName }, { "scale", 1 } };
            var result = database.RunCommand<BsonDocument>(command);
            int size = result["size"].AsInt32;
            int count = result["count"].AsInt32;
            Double ok = result["ok"].AsDouble;
            DataTable tb = new DataTable();
            tb.Columns.Add("Name");
            tb.Columns.Add("Value");

            tb.Rows.Add("Size", size.ToString());
            tb.Rows.Add("Count", count.ToString());
            tb.Rows.Add("State", ok.ToString());
            return tb;
        }

        public void ImportCollection(String collectionName)
        {
            string text = System.IO.File.ReadAllText(@"Hang.JSON");

            var document = BsonSerializer.Deserialize<Hang>(text);
            var collection = database.GetCollection<Hang>("Hang");
            collection.InsertOneAsync(document);
        }

        //public int GetCollectionCount(String collectionName)
        //{
        //    var command = new CommandDocument { { "collstats", collectionName }, { "scale", 1 } };
        //    var result = database.RunCommand<BsonDocument>(command);
        //    return result["count"].AsInt32;
        //}

        //public Boolean GetCollectionOk(String collectionName)
        //{
        //    var command = new CommandDocument { { "collstats", collectionName }, { "scale", 1 } };
        //    var result = database.RunCommand<BsonDocument>(command);
        //}

        //public BsonDocument GetCollectionDetails(String collectionName)
        //{
        //    var project = Builders<BsonDocument>.Projection.Include("ns").Include("size").Include("count").Include("ok");

        //    var command = new CommandDocument { { "collstats", collectionName }, { "scale", 1 } };
        //    var result = database.RunCommand<BsonDocument>(command);
        //    return result;
        //}

        //public BsonDocument GetInfoDatabase()
        //{
        //    var command = new CommandDocument { { "dbStats", 1 }, { "scale", 1 } };
        //    var result = database.RunCommand<BsonDocument>(command);
        //    return result;
        //}

        //public BsonDocument Get()
        //{
        //    var command = new BsonDocument
        //    {
        //        { "find", "HoaDon" },
        //    };
        //    //var result = database.RunCommand<BsonDocument>(command);
        //    return result;
        //}
    }
}
