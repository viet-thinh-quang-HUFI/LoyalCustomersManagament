using DAL;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using ZstdSharp.Unsafe;

namespace BLL
{
    public class NhanVienBLL
    {
        NhanVienDAL nhanVienDAL = new NhanVienDAL();
        IMongoCollection<BsonDocument> collection;

        public NhanVienBLL()
        {
            collection = nhanVienDAL.GetNhanVien();
        }

        public Byte Login(NhanVien nhanVien)
        {
            if (nhanVien.EmailNV == String.Empty || nhanVien.Matkhau == String.Empty)
            {
                return 1;
            }
            else
            {
                foreach (BsonDocument document in collection.Find(new BsonDocument()).ToList())
                {
                    if (document["EmailNV"] == nhanVien.EmailNV && document["Matkhau"] == nhanVien.Matkhau)
                    {
                        return 0;
                    }
                }
            }
            return 2;
        }

        public BsonDocument CheckExistedAccountName(NhanVien nhanVien)
        {
            if (nhanVien.EmailNV == String.Empty)
            {
                return null;
            }
            else
            {
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("EmailNV", nhanVien.EmailNV);

                BsonDocument document = collection.Find(filter).FirstOrDefault();

                try
                {
                    if (document.Count() != 0)
                    {
                        return document;
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public Byte ResetPassword(BsonDocument document, String newPassword, List<String> maKH)
        {
            int count = 0;
            if (newPassword == String.Empty)
            {
                return 1;
            }
            else
            {
                Array rs = document["MaKH"].AsBsonArray.ToArray();
                foreach (var choose in maKH)
                {
                    foreach (var myKH in rs)
                    {
                        if (choose.ToString() == myKH.ToString())
                        {
                            count++;
                            break;
                        }
                    }
                    if (count == 2)
                    {
                        return 0;
                    }
                }
                return 2;
            }
        }
    }
}
