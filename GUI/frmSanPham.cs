using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class frmSanPham : Form
    {
        SanPhamBLL SanPhamBLL = new SanPhamBLL();
        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = SanPhamBLL.GetSanPham();
            //SanPhamBLL.GetMoTa("");
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            MoTa moTa = new MoTa();
            string maSP = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            moTa = SanPhamBLL.GetMoTa(maSP);
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

            //SanPham s = new SanPham(txtMa.Text, txtTen.Text,Convert.ToInt32(txtGia.Text), Convert.ToInt32(txtSLT.Text), txtHang.Text);
            //SanPhamBLL.Them(s);

        }
    }
}
