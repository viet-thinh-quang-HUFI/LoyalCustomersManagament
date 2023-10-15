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

        public List<HoaDon> GetHoaDon(DateTime ngaydau, DateTime ngaycuoi)
        {
            int nd = ToUnixTimeSeconds(ngaydau);
            int nc = ToUnixTimeSeconds(ngaycuoi);
            var builder = Builders<HoaDon>.Filter;
            var filter = builder.And(Builders<HoaDon>.Filter.Gte(f => f.NgayLap, nd), Builders<HoaDon>.Filter.Lte(f => f.NgayLap, nc));
            List<HoaDon> hoaDons = hoaDonDAL.GetHoaDon().Find(filter).ToList();
            return hoaDons;
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
