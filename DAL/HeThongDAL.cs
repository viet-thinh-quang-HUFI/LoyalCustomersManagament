using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HeThongDAL
    {
        KetNoi conn = new KetNoi();
        private IMongoDatabase database;


        public HeThongDAL() {
            database = conn.Database;
        }

        public IMongoDatabase GetDatabase()
        {
            return database;
        }
    }
}
