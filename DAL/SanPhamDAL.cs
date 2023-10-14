using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Runtime.Documents;
using MongoDB.Driver.Core.Configuration;
using DTO;
using System.Collections.ObjectModel;

namespace DAL
{
    public class SanPhamDAL
    {
        KetNoi conn = new KetNoi();
        IMongoCollection<SanPham> collection;
        public SanPhamDAL() 
        {
            collection = conn.Database.GetCollection<SanPham>("SanPham");
        }

        public IMongoCollection<SanPham> GetSanPham()
        { 
            return collection;
        }

        public void Them(SanPham document)
        {
            collection.InsertOne(document);
        }
    }
}
