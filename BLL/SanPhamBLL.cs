using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using MongoDB.Bson.Serialization;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson.IO;

namespace BLL
{
    public class SanPhamBLL
    {
        SanPhamDAL sanPhamDAL = new SanPhamDAL();
        IMongoCollection<SanPham> collection;

        public SanPhamBLL()
        {
            collection = sanPhamDAL.GetSanPham();
        }

        public IMongoCollection<SanPham> GetSP()
        {
            return collection;
        }

        public IMongoCollection<SanPham> GetSanPham()
        {
            var filter = Builders<SanPham>.Filter.Empty;
            var sanPhams = sanPhamDAL.GetSanPham()
                .Find(filter)
                .ToList();
            return sanPhamDAL.GetSanPham();
        }
        public MoTa GetMoTa(string maSP)
        {
            MoTa moTa = new MoTa();
            //MongoCollection<BsonDocument> coll = SanPhamDAL.GetMoTa();

            var filter = Builders<SanPham>.Filter.Eq(a => a.MaSP, maSP);

            try
            {
                moTa = collection.Find(filter).SingleOrDefault().MoTa;
            }
            catch (Exception e)
            {
                return null;
            }
            return moTa;
        }
        public string Them(string ma, string ten, string dongia, string sl, string hang)
        {
            if (ma == "")
            {
                return "Chưa nhập mã! ";
            }
            if (ten == "")
            {
                return "Chưa nhập tên! ";
            }
            if (dongia == "")
            {
                return "Chưa nhập đơn giá! ";
            }
            if (sl == "")
            {
                return "Chưa nhập số lượng tồn! ";
            }
            if (hang == "")
            {
                return "Chưa nhập hãng! ";
            }
            if (IsNumber(sl) == false || Convert.ToInt32(sl) < 0)
            {
                return "Nhập số lượng tồn sai";
            }
            if (IsNumber(dongia) == false || Convert.ToInt32(dongia) < 0)
            {
                return "Nhập đơn giá sai ";
            }
            var sp = new SanPham
            {
                MaSP = ma, TenSP = ten, Dongia = Convert.ToInt32(dongia), Soluongton = Convert.ToInt32(sl), Mahang = hang
            };
            sanPhamDAL.Them(sp);
            return "Thêm thành công";
        }
        public string Xoa(string ma)
        {
            var deleteFilter = Builders<SanPham>.Filter.Eq(a => a.MaSP, ma);
            collection.DeleteOne(deleteFilter);
            return "Xóa thành công";
        }
        public string Sua(string ma, string ten, string dongia, string sl, string hang)
        {
            if (ma == "")
            {
                return "Chưa nhập mã! ";
            }
            if (ten == "")
            {
                return "Chưa nhập tên! ";
            }
            if (dongia == "")
            {
                return "Chưa nhập đơn giá! ";
            }
            if (sl == "")
            {
                return "Chưa nhập số lượng tồn! ";
            }
            if (hang == "")
            {
                return "Chưa nhập hãng! ";
            }
            if (IsNumber(sl) == false || Convert.ToInt32(sl) < 0)
            {
                return "Nhập số lượng tồn sai";
            }
            if (IsNumber(dongia) == false || Convert.ToInt32(dongia) < 0)
            {
                return "Nhập đơn giá sai ";
            }
            var filter = Builders<SanPham>.Filter.Eq(a => a.MaSP, ma);
            var update = Builders<SanPham>.Update
                .Set(a => a.TenSP, ten)
                .Set(a => a.Dongia, Convert.ToInt32(dongia))
                .Set(a => a.Soluongton, Convert.ToInt32(sl))
                .Set(a => a.Mahang, hang);
            collection.UpdateOne(filter, update);
            return "Sửa thành công";
        }
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        public Byte DeleteAllSanPham()
        {
            try
            {
                var filter = Builders<SanPham>.Filter.Empty;
                collection.DeleteMany(filter);
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public async Task ExportSanPham(String fileName, Action<bool> callBack)
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

        public async Task ImportSanPham(String fileName, Action<bool> callBack)
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
