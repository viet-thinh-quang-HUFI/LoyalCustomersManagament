using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoaDon
    {
        private String _id;
        private String _maHD;
        private Int64 _ngayLap;
        private List<ndHoaDon> _hoaDon;

        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get => _id; set => _id = value; }

        [BsonElement("MaHD"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string MaHD { get => _maHD; set => _maHD = value; }

        [BsonElement("Ngaylaptimestamp"), BsonRepresentation(MongoDB.Bson.BsonType.Int64)]
        public Int64 NgayLap { get => _ngayLap; set => _ngayLap = value; }

        [BsonElement("Sanpham")]
        public List<ndHoaDon> Hoadon { get => _hoaDon; set => _hoaDon = value; }

        public HoaDon() { }
    }
}
