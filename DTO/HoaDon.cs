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
        private Int32 _ngayLap;
        private Int32 _tongTien;
        private List<ndHoaDon> _hoaDon;

        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get => _id; set => _id = value; }

        [BsonElement("MaHD"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string MaHD { get => _maHD; set => _maHD = value; }

        [BsonElement("Ngaylaptimestamp"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public Int32 NgayLap { get => _ngayLap; set => _ngayLap = value; }

        [BsonElement("Tongtien"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int TongTien { get => _tongTien; set => _tongTien = value; }

        [BsonElement("Sanpham")]
        public List<ndHoaDon> Hoadon { get => _hoaDon; set => _hoaDon = value; }

       

        public HoaDon() { }
    }
}
