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
    }
}
