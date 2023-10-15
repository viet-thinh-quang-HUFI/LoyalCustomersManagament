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
    public class HangBLL
    {
        HangDAL hangDAL = new HangDAL();
        IMongoCollection<Hang> collection;

        public HangBLL()
        {
            collection = hangDAL.GetHang();
        }

        public Byte DeleteAllHang()
        {
            try
            {
                var filter = Builders<Hang>.Filter.Empty;
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
