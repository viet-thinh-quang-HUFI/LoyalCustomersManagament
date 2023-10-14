using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using BLL;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace GUI
{
    public partial class frmSanPham : Form
    {
        SanPhamBLL sanPhamBLL = new SanPhamBLL();
        public frmSanPham()
        {
            InitializeComponent();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            MoTa moTa = new MoTa();
            string maSP = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            moTa = sanPhamBLL.GetMoTa(maSP);
            if (moTa != null)
            {
                txtHieuNang.Text = moTa.HieuNang.ToString();
                txtKichThuoc.Text = moTa.KichThuoc.ToString();
                txtTrongLuong.Text = moTa.TrongLuong.ToString();
            }
            else
            {
                txtHieuNang.Text = "";
                txtKichThuoc.Text = "";
                txtTrongLuong.Text = "";
            }
            txtMa.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtTen.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtGia.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtSLT.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtHang.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        //test TimestampToDateTime
        //private DateTime TimestampToDateTime(long timestamp)
        //{
        //    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //    return origin.AddSeconds(timestamp);
        //}

        private void btnThem_Click(object sender, EventArgs e)
        {
            //DateTime d = TimestampToDateTime(1665592827);
            //txtMa.Text = d.ToString();

            string kq = sanPhamBLL.Them(txtMa.Text, txtTen.Text, txtGia.Text, txtSLT.Text, txtHang.Text);
            MessageBox.Show(kq);
            dataGridView1.DataSource = sanPhamBLL.GetSanPham();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            ////db.SanPham.aggregate([
            ////  {$project: { _id: 0,MaSP: 1,TenSP: 1,Dongia: 1,Soluongton: 1,Mahang: 1,Tinhtrang: 1} }
            ////])

            //int start = 0;
            //int end = 2;


            ProjectionDefinition<SanPham> simpleProjection = Builders<SanPham>.Projection
                .Exclude(u => u.Id)
                .Include(u => u.MaSP)
                .Include(u => u.TenSP);

            PipelineDefinition<SanPham, BsonDocument> pipeline = new BsonDocument[]
            {
                new BsonDocument("$Project", "$MaSP"),
            };

            var filter = Builders<SanPham>.Filter.Empty;
            var projection = Builders<SanPham>.Projection.Include("MaSP")
                .Include("TenSP")
                .Include("Dongia")
                .Include("Soluongton")
                .Include("Mahang")
                .Include("Tinhtrang")
                .Exclude("_id");

            List<SanPham> a = sanPhamBLL.GetSanPham().Find(filter).Project<SanPham>(projection).ToList();
            dataGridView1.DataSource = a;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Mota"].Visible = false;


            //SanPhamBLL.GetMoTa("");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (ma == "")
            {
                MessageBox.Show("Chưa chọn khách hàng cần xóa");
            }
            else
            {
                SanPhamBLL.Xoa(ma);
                dataGridView1.DataSource = SanPhamBLL.GetSanPham();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string kq = SanPhamBLL.Sua(txtMa.Text, txtTen.Text, txtGia.Text, txtSLT.Text, txtHang.Text);
            MessageBox.Show(kq);
            dataGridView1.DataSource = SanPhamBLL.GetSanPham();
        }
    }
}
