using DAL;
using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List <HoaDon> hoaDons= hoaDonDAL.GetHoaDon().Find(filter).ToList();
            return hoaDons;
        }

        public static int ToUnixTimeSeconds(DateTime date)
        {
            DateTime point = new DateTime(1970, 1, 1);
            TimeSpan time = date.Subtract(point);

            return (int)time.TotalSeconds;
        }
    }
}
