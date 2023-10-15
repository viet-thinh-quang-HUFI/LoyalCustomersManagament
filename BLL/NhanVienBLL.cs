using DAL;
using DTO;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace BLL
{
    public class NhanVienBLL
    {
        NhanVienDAL nhanVienDAL = new NhanVienDAL();
        KhachHangDAL khachHangDAL = new KhachHangDAL();
        IMongoCollection<NhanVien> collectionNV;
        IMongoCollection<KhachHang> collectionKH;

        public NhanVienBLL()
        {
            collectionNV = nhanVienDAL.GetNhanVien();
        }

        public IMongoCollection<NhanVien> GetNV()
        {
            return collectionNV;
        }

        public List<NhanVien> GetNhanVien()
        {
            var filter = Builders<NhanVien>.Filter.Empty;
            var nhanViens = nhanVienDAL.GetNhanVien()
                .Find(filter)
                .ToList();
            return nhanViens;
        }

        public void GetListKHsOfNV(String maNV)
        {
            collectionKH = khachHangDAL.GetKhachHang();

            var matchMaNV = Builders<NhanVien>.Filter.Eq(a => a.MaNV, maNV);
            var lookup = new BsonDocument { { "$lookup", new BsonDocument { { "from", "KhachHang" }, { "localField", "MaKH" }, { "foreignField", "MaKH" }, { "as", "ThongtinKH" } } } };

            //var rs = collectionNV.Aggregate()
            //    .Match(matchMaNV)
            //    .Unwind("MaKH")
                //.Lookup<String, String, NhanVienLookedUp>(collectionKH,
                //"",
                //"",
                //i => i.KhachHangList).ToList();
            //.Lookup(lookup)
            //.Project(Builders<BsonDocument>.Projection.Exclude("ThongtinKH"))
            //.Unwind("ThongtinKH").ToList();
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

                var result = collectionNV.Find(filter).ToList();
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
                    String maNV = collectionNV.Find(filter).SingleOrDefault().MaNV;
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
            if (nv == null)
            {
                return null;
            }
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
                collectionNV.DeleteMany(filter);
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


            var rs = collectionNV.Find(filter).SingleOrDefault();
            if (rs == null)
                return false;
            return true;
        }

        public async Task ExportNhanVien(String fileName, Action<bool> callBack)
        {
            bool result = false;

            string outputFileName = @"" + fileName;

            using (var streamWriter = new StreamWriter(outputFileName))
            {
                try
                {
                    await collectionNV.Find(new BsonDocument())
                    .ForEachAsync(async (document) =>
                    {
                        using (var stringWriter = new StringWriter())
                        using (var jsonWriter = new JsonWriter(stringWriter))
                        {
                            var context = BsonSerializationContext.CreateRoot(jsonWriter);
                            collectionNV.DocumentSerializer.Serialize(context, document);
                            var line = stringWriter.ToString();
                            await streamWriter.WriteLineAsync(line);
                        }
                    });
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            if (callBack != null) callBack(result);
        }

        public async Task ImportNhanVien(String fileName, Action<bool> callBack)
        {
            bool result = false;

            string inputFileName = @"" + fileName;

            using (var streamReader = new StreamReader(inputFileName))
            {
                string line;
                try
                {
                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        using (var jsonReader = new JsonReader(line))
                        {
                            var context = BsonDeserializationContext.CreateRoot(jsonReader);
                            var document = collectionNV.DocumentSerializer.Deserialize(context);
                            await collectionNV.InsertOneAsync(document);
                        }
                    }
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            if (callBack != null) callBack(result);
        }
    }
}
