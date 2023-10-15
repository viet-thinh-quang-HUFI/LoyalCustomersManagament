using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HoaDonDAL
    {
        KetNoi conn = new KetNoi();
        private IMongoCollection<HoaDon> collection;
        public HoaDonDAL()
        {
            collection = conn.Database.GetCollection<HoaDon>("HoaDon");
        }
        public IMongoCollection<HoaDon> GetHoaDon()
        {
            return collection;
        }

    }
}
