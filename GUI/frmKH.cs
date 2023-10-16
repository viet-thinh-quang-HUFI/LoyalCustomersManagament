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
        KhachHangBLL khachHangBLL = new KhachHangBLL();
        public frmKH()
        {
            InitializeComponent();
        }

        private void load()
        {
            dataGridView1.DataSource = khachHangBLL.GetKhachHang();
            dataGridView1.Columns["id"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string kq = khachHangBLL.Them(txtMa.Text,txtTen.Text,txtTuoi.Text,txtSDT.Text,txtEmail.Text,txtDiem.Text);
            MessageBox.Show(kq);
            load();
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
            string ma = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (ma == "")
            {
                MessageBox.Show("Chưa chọn khách hàng cần xóa");
            }
            else
            {
                khachHangBLL.Xoa(ma);
                load();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string kq = khachHangBLL.Sua(txtMa.Text, txtTen.Text, txtTuoi.Text, txtSDT.Text, txtEmail.Text, txtDiem.Text);
            MessageBox.Show(kq);
            load();
        }

        private void frmKH_Load(object sender, EventArgs e)
        {
            load();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if(txtSDT.Text == "")
            {
                MessageBox.Show("Chưa nhập số điện thoại");
            }
            else if(khachHangBLL.GetKHtheoSDT(txtSDT.Text)==null)
            {
                MessageBox.Show("Không tìm thấy");
            }
            else {
                dataGridView1.DataSource = khachHangBLL.GetKHtheoSDT(txtSDT.Text);
            }
              
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Chưa nhập email");
            }
            else if (khachHangBLL.GetKHtheoEmail(txtEmail.Text) == null)
            {
                MessageBox.Show("Không tìm thấy");
            }
            else
            {
                dataGridView1.DataSource = khachHangBLL.GetKHtheoEmail(txtEmail.Text);
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (txtTen.Text == "")
            {
                MessageBox.Show("Chưa nhập tên khách hàng");
            }
            else if (khachHangBLL.GetKHtheoTen(txtTen.Text) == null)
            {
                MessageBox.Show("Không tìm thấy");
            }
            else
            {
                dataGridView1.DataSource = khachHangBLL.GetKHtheoTen(txtTen.Text);
            }
        }
    }
}
