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
            String email = nhanVien.EmailNV;
            String password = nhanVien.Matkhau;

            if (email == String.Empty || password == String.Empty)
            {
                return 1;
            }
            else
            {
                var filter = Builders<NhanVien>.Filter.And(
                    Builders<NhanVien>.Filter.Eq(a => a.EmailNV, email),
                    Builders<NhanVien>.Filter.Eq(b => b.Matkhau, password));

                var result = collection.Find(filter).ToList();
                if (result.Count > 0)
                {
                    return 0;
                }
            }
            return 2;
        }

        public String CheckExistedAccountName(String emailKH)
        {
            String email = emailKH;

            if (email == String.Empty)
            {
                return null;
            }
            else
            {
                var filter = Builders<NhanVien>.Filter.Eq(a => a.EmailNV, email);
                var project = Builders<NhanVien>.Projection.Include(x => x.MaNV);

                try
                {
                    String maNV = collection.Find(filter).SingleOrDefault().MaNV;
                    return maNV;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Byte ResetPassword(String maNV, String newPassword, String authencationName)
        {
            if (newPassword == String.Empty)
            {
                return 1;
            }
            else
            {
                if (authencationName == maNV)
                {
                    nhanVienDAL.UpdatePasswordNhanVien(authencationName, newPassword);
                    return 0;
                }
                return 2;
            }
        }
        public List<KhachHang> GetKHtheoNV(string mail)
        {
            var filter = Builders<NhanVien>.Filter.Eq(a => a.EmailNV, mail);
            var nv = nhanVienDAL.GetNhanVien().Find(filter).SingleOrDefault().MaKH;
            KhachHangBLL khachHangBLL = new KhachHangBLL();
            List<KhachHang> khachHangs = new List<KhachHang>();
            for (int i = 0; i < nv.Count; i++)
            {
                KhachHang kh = khachHangBLL.GetMotKH(nv[i]);
                khachHangs.Add(kh);
            }
            return khachHangs;
        }

        public Byte DeleteAllNhanVien()
        {
            try
            {
                var filter = Builders<NhanVien>.Filter.Empty;
                collection.DeleteMany(filter);
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public Boolean CheckIsAdmin(String emailNV)
        {
            String email = emailNV;

            var filter = Builders<NhanVien>.Filter.And(
                Builders<NhanVien>.Filter.Eq(a => a.EmailNV, email),
                Builders<NhanVien>.Filter.Eq(a => a.IsAdmin, true));


            var rs = collection.Find(filter).SingleOrDefault();
            if (rs == null)
                return false;
            return true;
        }
    }
}
