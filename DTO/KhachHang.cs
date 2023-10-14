using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KhachHang
    {
        private String _id;
        private String _maKH;
        private String _hoTen;
        private Int32 _tuoi;
        private String _sdt;
        private String _email;
        private Double _diem;
        private List<String> _hoaDon;
        public KhachHang() { }
        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get => _id; set => _id = value; }

        [BsonElement("MaKH"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string MaKH { get => _maKH; set => _maKH = value; }

        [BsonElement("Hoten"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Hoten { get => _hoTen; set => _hoTen = value; }

        [BsonElement("Tuoi"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int Tuoi { get => _tuoi; set => _tuoi = value; }

        [BsonElement("SDT"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string SDT { get => _sdt; set => _sdt = value; }

        [BsonElement("EmailKH"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string EmailKH { get => _email; set => _email = value; }

        [BsonElement("Diem"), BsonRepresentation(MongoDB.Bson.BsonType.Double)]
        public Double Diem { get => _diem; set => _diem = value; }
        
        [BsonElement("Hoadon")]
        public List<string> Hoadon { get => _hoaDon; set => _hoaDon = value; }
    }
}
