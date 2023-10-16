using DAL;
using DTO;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ValidTextLibrary;

namespace BLL
{
    public class NhanVienBLL
    {
        NhanVienDAL nhanVienDAL = new NhanVienDAL();
        KhachHangDAL khachHangDAL = new KhachHangDAL();
        IMongoCollection<NhanVien> collectionNV;
        IMongoCollection<KhachHang> collectionKH;
        ValidText validText = new ValidText();

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

        public Byte AddNhanVien(NhanVien nhanVien)
        {
            if (nhanVien.MaNV == String.Empty || nhanVien.EmailNV == String.Empty || nhanVien.Matkhau == String.Empty)
            {
                return 1;
            }
            else if (!validText.IsValidEmail(nhanVien.EmailNV))
            {
                return 2;
            }
            else
            {
                try
                {
                    collectionNV.InsertOne(nhanVien);
                    return 0;
                }
                catch
                {
                    return 3;

                }
            }
        }

        public Byte DeleteNhanVien(String maNV)
        {
            if (maNV == String.Empty)
            {
                return 1;
            }
            else
            {
                if (CheckPrimaryKeyNV(maNV) == false)
                {
                    collectionNV.DeleteOne(a => a.MaNV == maNV);
                    return 0;
                }
                else
                {
                    return 2;
                }
            }
        }

        public Byte UpdateNhanVien(NhanVien nhanVien)
        {
            if (nhanVien.MaNV == String.Empty || nhanVien.EmailNV == String.Empty || nhanVien.Matkhau == String.Empty)
            {
                return 1;
            }
            else if (!validText.IsValidEmail(nhanVien.EmailNV))
            {
                return 2;
            }
            else
            {
                if (CheckPrimaryKeyNV(nhanVien.MaNV) == false)
                {
                    var filter = Builders<NhanVien>.Filter.Eq(a => a.MaNV, nhanVien.MaNV);
                    var update = Builders<NhanVien>.Update
                        .Set(a => a.HotenNV, nhanVien.HotenNV)
                        .Set(a => a.EmailNV, nhanVien.EmailNV)
                        .Set(a => a.Matkhau, nhanVien.Matkhau)
                        .Set(a => a.KPI, nhanVien.KPI)
                        .Set(a => a.IsAdmin, nhanVien.IsAdmin);
                    collectionNV.UpdateOne(filter, update);
                    return 0;
                }
                else
                {
                    return 3;
                }
            }
        }

        public Boolean CheckPrimaryKeyNV(String maNV)
        {
            String ma = maNV;

            if (ma == String.Empty)
            {
                return false;
            }
            else
            {
                var filter = Builders<NhanVien>.Filter.Eq(a => a.MaNV, ma);
                var project = Builders<NhanVien>.Projection.Include(x => x.MaNV);

                try
                {
                    String rs = collectionNV.Find(filter).SingleOrDefault().MaNV;
                    return false;
                }
                catch
                {
                    return true;
                }
            }
        }

        public List<NhanVien> GetTopKPI()
        {
            try
            {
                var filter = Builders<NhanVien>.Filter.Empty;

                var rs = collectionNV.Find(filter).SortByDescending(x => x.KPI).Limit(3).ToList();

                return rs;
            }
            catch
            {
                return null;
            }
        }

        public List<NhanVien> SearchByTen(String query)
        {
            var filter = Builders<NhanVien>.Filter.Regex("HotenNV", new BsonRegularExpression(query));
            var rs = collectionNV.Find(filter).ToList();

            return rs;
        }

        public List<NhanVien> SearchByEmail(String query)
        {
            var filter = Builders<NhanVien>.Filter.Regex("EmailNV", new BsonRegularExpression(query));
            var rs = collectionNV.Find(filter).ToList();

            return rs;
        }

        public DataTable GetListKHsOfNV(String maNV)
        {
            IEnumerable<KhachHang> allKHs;
            DataTable tb = new DataTable();
            tb.Columns.Add("Mã khách hàng");
            tb.Columns.Add("Tên khách hàng");
            tb.Columns.Add("Số điểm");
            tb.Columns.Add("Cấp bậc");
            tb.Columns.Add("Số điện thoại");
            tb.Columns.Add("Email");

            try
            {
                var rs = collectionNV.Aggregate()
                .Match(new BsonDocument { { "MaNV", maNV } })
                .Unwind("MaKH")
                .Lookup("KhachHang", "MaKH", "MaKH", "Thongtin")
                .Unwind("Thongtin")
                .Group(new BsonDocument{
                        { "_id", "$MaNV" },
                        {
                            "Danhsach", new BsonDocument{
                                { "$addToSet", "$Thongtin" }
                            }
                        }
                    })
                .Project(new BsonDocument{
                    { "_id", 0 },
                    { "Danhsach", 1 },
                }).First();
                allKHs = BsonSerializer.Deserialize<IEnumerable<KhachHang>>(rs[0].ToString());
            }
            catch
            {
                tb.Rows.Add("", "", "", "", "", "");
                return tb;
            }

            List<KhachHang> abc = new List<KhachHang>();
            foreach (var item in allKHs)
            {
                String maKh = item.MaKH.ToString();
                String hoTen = item.Hoten.ToString();
                Double diem = item.Diem;
                String capBac;
                if (diem <= 100) capBac = "Đồng";
                else if (diem <= 1000)
                    capBac = "Bạc";
                else
                    capBac = "Vàng";
                String sdt = item.SDT.ToString();
                String email = item.EmailKH.ToString();

                tb.Rows.Add(maKh, hoTen, diem.ToString(), capBac, sdt, email);
            }
            return tb;
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
