using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Hang
    {
        private String _id;
        private String _maHang;
        private String _tenHang;

        public Hang() { }

        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get => _id; set => _id = value; }

        [BsonElement("Mahang"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string MaHang { get => _maHang; set => _maHang = value; }

        [BsonElement("Tenhang"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string TenHang { get => _tenHang; set => _tenHang = value; }
    }
}
