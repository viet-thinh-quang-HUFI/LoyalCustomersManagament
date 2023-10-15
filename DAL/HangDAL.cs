using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HangDAL
    {
        KetNoi conn = new KetNoi();
        private IMongoCollection<Hang> collection;

        public HangDAL()
        {
            collection = conn.Database.GetCollection<Hang>("Hang");
        }

        public IMongoCollection<Hang> GetHang()
        {
            return collection;
        }
    }
}
