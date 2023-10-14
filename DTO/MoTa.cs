using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MoTa
    {
       
        private Double _kichThuoc;
        private string _hieuNang;
        private Int32 _trongLuong;

        [BsonElement("Kichthuoc"), BsonRepresentation(MongoDB.Bson.BsonType.Double)]
        public double KichThuoc { get => _kichThuoc; set => _kichThuoc = value; }

        [BsonElement("Hieunang"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string HieuNang { get => _hieuNang; set => _hieuNang = value; }
        
        [BsonElement("Trongluong"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int TrongLuong { get => _trongLuong; set => _trongLuong = value; }
        

        public MoTa() { }
    }
}
