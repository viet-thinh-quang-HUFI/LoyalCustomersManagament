using DAL;
using DTO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.IO;
using System.Collections.ObjectModel;
using ValidTextLibrary;

namespace BLL
{
    public class HoaDonBLL
    {
        HoaDonDAL hoaDonDAL = new HoaDonDAL();
        IMongoCollection<HoaDon> collection;

        public HoaDonBLL()
        {
            collection = hoaDonDAL.GetHoaDon();
        }

        public Byte Insert(HoaDon hoaDon)
        {
            if (hoaDon.MaHD == String.Empty || hoaDon.Hoadon.Count == 0)
            {
                return 1;
            }
            else
            {
                try
                {
                    collection.InsertOne(hoaDon);
                    return 0;
                }
                catch
                {
                    return 2;

                }
            }
        }

        public List<HoaDon> GetHoaDon(DateTime ngaydau, DateTime ngaycuoi)
        {
            int nd = ToUnixTimeSeconds(ngaydau);
            int nc = ToUnixTimeSeconds(ngaycuoi);
            var builder = Builders<HoaDon>.Filter;
            var filter = builder.And(Builders<HoaDon>.Filter.Gte(f => f.NgayLap, nd), Builders<HoaDon>.Filter.Lte(f => f.NgayLap, nc));
            List<HoaDon> hoaDons = hoaDonDAL.GetHoaDon().Find(filter).ToList();
            return hoaDons;
        }
        public HoaDon GetMotHoaDon(string ma)
        {
            var filter = Builders<HoaDon>.Filter.Eq(f => f.MaHD, ma);
            HoaDon hoaDon = hoaDonDAL.GetHoaDon().Find(filter).FirstOrDefault();
            return hoaDon;
        }

        public List<SanPham> GetSPtheoHD(string ma)
        {
            var filter = Builders<HoaDon>.Filter.Eq(a => a.MaHD, ma);
            var hd = hoaDonDAL.GetHoaDon().Find(filter).SingleOrDefault().Hoadon;
            SanPhamBLL sanPhamBLL = new SanPhamBLL();
            List<SanPham> sanPhams = new List<SanPham>();
            if (hd == null)
            {
                return null;
            }
            for (int i = 0; i < hd.Count; i++)
            {
                SanPham sp = sanPhamBLL.GetMotSanPham(hd[i].MaSP);
                sanPhams.Add(sp);
            }
            return sanPhams;
        }
        public static int ToUnixTimeSeconds(DateTime date)
        {
            DateTime point = new DateTime(1970, 1, 1);
            TimeSpan time = date.Subtract(point);

            return (int)time.TotalSeconds;
        }

        public Byte DeleteAllHoaDon()
        {
            try
            {
                var filter = Builders<HoaDon>.Filter.Empty;
                collection.DeleteMany(filter);
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public IMongoCollection<HoaDon> GetHD()
        {
            return collection;
        }

        public async Task ExportHoaDon(String fileName, Action<bool> callBack)
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

        public async Task ImportHoaDon(String fileName, Action<bool> callBack)
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
