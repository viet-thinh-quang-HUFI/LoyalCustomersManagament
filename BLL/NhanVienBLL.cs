using DAL;
using DTO;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;
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

        public IMongoCollection<NhanVien> GetNV()
        {
            return collection;
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
            if(nv == null)
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

        public async Task ExportNhanVien(String fileName, Action<bool> callBack)
        {
            bool result = false;

            string outputFileName = @"" + fileName;

            using (var streamWriter = new StreamWriter(outputFileName))
            {
                try
                {
                    await collection.Find(new BsonDocument())
                    .ForEachAsync(async (document) =>
                    {
                        using (var stringWriter = new StringWriter())
                        using (var jsonWriter = new JsonWriter(stringWriter))
                        {
                            var context = BsonSerializationContext.CreateRoot(jsonWriter);
                            collection.DocumentSerializer.Serialize(context, document);
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

            string inputFileName = @"C:\NhanVien.json";

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
                            var document = collection.DocumentSerializer.Deserialize(context);
                            await collection.InsertOneAsync(document);
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
