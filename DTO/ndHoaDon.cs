using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ndHoaDon
    {
        private string _masp;
        private int _sl;

        [BsonElement("MaSP"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string MaSP { get => _masp; set => _masp = value; }

        [BsonElement("Soluong"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int Sl { get => _sl; set => _sl = value; }
    }
}
