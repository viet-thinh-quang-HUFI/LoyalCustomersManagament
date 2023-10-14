﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class NhanVien
    {
        private String _id;
        private String _MaNV;
        private String _HotenNV;
        private String _EmailNV;
        private String _Matkhau;
        private Int16 _KPI;
        private List<String> _MaKH;

        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get => _id; set => _id = value; }
        [BsonElement("MaNV"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string MaNV { get => _MaNV; set => _MaNV = value; }
        [BsonElement("HotenNV"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string HotenNV { get => _HotenNV; set => _HotenNV = value; }

        [BsonElement("EmailNV"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string EmailNV { get => _EmailNV; set => _EmailNV = value; }

        [BsonElement("Matkhau"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Matkhau { get => _Matkhau; set => _Matkhau = value; }
        [BsonElement("KPI"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public short KPI { get => _KPI; set => _KPI = value; }
        [BsonElement("MaKH")]
        public List<string> MaKH { get => _MaKH; set => _MaKH = value; }

        //public List<string> MaKH { get => MaKH1; set => MaKH1 = value; }

        public NhanVien()
        {

        }
    }
}
