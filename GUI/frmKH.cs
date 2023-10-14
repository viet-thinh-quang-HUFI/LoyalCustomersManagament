using BLL;
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
    public partial class frmKH : Form
    {
        KhachHangBLL KhachHangBLL = new KhachHangBLL();
        public frmKH()
        {
            InitializeComponent();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = KhachHangBLL.GetKhachHang();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string kq = KhachHangBLL.Them(txtMa.Text,txtTen.Text,txtTuoi.Text,txtSDT.Text,txtEmail.Text,txtDiem.Text);
            MessageBox.Show(kq);
            dataGridView1.DataSource = KhachHangBLL.GetKhachHang();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            txtMa.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtTen.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTuoi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtSDT.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtDiem.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (ma == "")
            {
                MessageBox.Show("Chưa chọn khách hàng cần xóa");
            }
            else
            {
                KhachHangBLL.xoa(ma);
                dataGridView1.DataSource = KhachHangBLL.GetKhachHang();
            }
        }
    }
}
