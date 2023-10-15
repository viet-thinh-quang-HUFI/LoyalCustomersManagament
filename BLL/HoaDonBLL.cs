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
    }
}
