using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhanVienDAL
    {
        KetNoi conn = new KetNoi();
        public NhanVienDAL() { }

        public IMongoCollection<BsonDocument> GetNhanVien()
        {
            IMongoCollection<BsonDocument> collection = conn.Database.GetCollection<BsonDocument>("NhanVien");
            return collection;
        }
    }
}
