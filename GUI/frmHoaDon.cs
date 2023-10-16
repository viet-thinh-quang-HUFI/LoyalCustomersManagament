using BLL;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmHoaDon : Form
    {
        SanPhamBLL sanPhamBLL = new SanPhamBLL();
        HoaDonBLL hoaDonBLL = new HoaDonBLL();
        List<ndHoaDon> sanPhams = new List<ndHoaDon>();
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = sanPhamBLL.GetALLSP();
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView2.AllowUserToAddRows = false;
        }

        private void btnCHoaDon_Click(object sender, EventArgs e)
        {
            String MaSP = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            int SoLuongMua = Convert.ToInt32(txtSoLuongMua.Value);
            if (SoLuongMua <= 0)
            {
                MessageBox.Show("Nhập sô lượng mua");
                return;
            }
            dataGridView2.Rows.Add(MaSP, SoLuongMua);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridView2.Rows)
            {
                ndHoaDon item = new ndHoaDon();
                item.MaSP = (string)dr.Cells[0].Value;
                item.Sl = Convert.ToInt32(dr.Cells[1].Value);

                sanPhams.Add(item);
            }

            HoaDon hd = new HoaDon();
            hd.MaHD = tbMaHD.Text;
            hd.Hoadon = sanPhams;

            var s = hoaDonBLL.Insert(hd);
            if (s == 0)
            {
                MessageBox.Show("Thanh cong");
            }
            else if (s == 1)
            {
                MessageBox.Show("Thêm đủ thông tin");
            }
            else
            {
                MessageBox.Show("Trúng khóa");
            }
        }

        private void btnCSanPham_Click(object sender, EventArgs e)
        {
            int position = dataGridView2.CurrentRow.Index;
            if (position > 0)
                dataGridView2.Rows.RemoveAt(position);
        }
    }
}
