using DAL;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using ZstdSharp.Unsafe;

namespace BLL
{
    public class NhanVienBLL
    {
        NhanVienDAL nhanVienDAL = new NhanVienDAL();
        IMongoCollection<NhanVien> collection;

        public NhanVienBLL()
        {
            collection = nhanVienDAL.GetNhanVien();
        }

        public List<NhanVien> GetNhanVien()
        {
            var filter = Builders<NhanVien>.Filter.Empty;
            var nhanViens = nhanVienDAL.GetNhanVien()
                .Find(filter)
                .ToList();
            return nhanViens;
        }

        public Byte Login(NhanVien nhanVien)
        {
            if (nhanVien.EmailNV == String.Empty || nhanVien.Matkhau == String.Empty)
            {
                return 1;
            }
            else
            {
                var filter = Builders<NhanVien>.Filter.Empty;
                foreach (NhanVien document in collection.Find(filter).ToList())
                {
                    if (document.EmailNV == nhanVien.EmailNV && document.Matkhau == nhanVien.Matkhau)
                    {
                        return 0;
                    }
                }
            }
            return 2;
        }

        public NhanVien CheckExistedAccountName(NhanVien nhanVien)
        {
            if (nhanVien.EmailNV == String.Empty)
            {
                return null;
            }
            else
            {
                var builder = Builders<NhanVien>.Filter;
                var filter = builder.Eq("EmailNV", nhanVien.EmailNV);

                NhanVien document = collection.Find(filter).FirstOrDefault();

                try
                {
                    if (document != null)
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

        public Byte ResetPassword(NhanVien document, String newPassword, String authencationName)
        {
            if (newPassword == String.Empty)
            {
                return 1;
            }
            else
            {
                if (authencationName == document.MaNV)
                {
                    nhanVienDAL.UpdatePasswordNhanVien(authencationName, newPassword);
                    return 0;
                }
                return 2;
            }
        }
    }
}
