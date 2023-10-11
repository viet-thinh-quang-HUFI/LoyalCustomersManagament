using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;


namespace LoyalCustomersManagament
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<SanPham> sanPhams = new List<SanPham>();   
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase("LoyalCustomersManagement");
            var collection = db.GetCollection<BsonDocument>("SanPham");
            foreach (BsonDocument document in collection.FindAll())
            {
                string ma = document["MaSP"].AsString;
                string ten = document["TenSP"].AsString;
                Int32 gia = document["Dongia"].AsInt32;
                Int32 sl = document["Soluongton"].AsInt32;
                string hang = document["Hang"].AsString;
                SanPham sanPham = new SanPham(ma, ten,gia,sl,hang);
                sanPhams.Add(sanPham);
            }
            dataGridView1.DataSource = sanPhams;
        }
    }
}
