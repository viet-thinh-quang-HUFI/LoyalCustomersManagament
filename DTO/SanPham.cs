using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SanPham
    {
        private String _id;
        private String _maSP;
        private String _tenSP;
        private Int32 _donGia;
        private Int32 _soLuongTon;
        private String _hang;
        private Boolean _tinhTrang;
        private MoTa _moTa;

        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get => _id; set => _id = value; }

        [BsonElement("MaSP"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string MaSP { get => _maSP; set => _maSP = value; }

        [BsonElement("TenSP"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string TenSP { get => _tenSP; set => _tenSP = value; }

        [BsonElement("Mahang"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Mahang { get => _hang; set => _hang = value; }

        [BsonElement("Dongia"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int Dongia { get => _donGia; set => _donGia = value; }

        [BsonElement("Soluongton"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int Soluongton { get => _soLuongTon; set => _soLuongTon = value; }

        [BsonElement("Tinhtrang"), BsonRepresentation(MongoDB.Bson.BsonType.Boolean)]
        public bool Tinhtrang { get => _tinhTrang; set => _tinhTrang = value; }

        [BsonElement("Mota")]
        public MoTa MoTa { get => _moTa; set => _moTa = value; }
        public SanPham()
        {
            //Id = null;
            //MoTa = null;
        }
    }
}
