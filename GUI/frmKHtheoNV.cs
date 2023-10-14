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
    public partial class frmKHtheoNV : Form
    {
        NhanVienBLL nhanVienBLL = new NhanVienBLL();
        public frmKHtheoNV()
        {
            InitializeComponent();
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

        private void frmKHtheoNV_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nhanVienBLL.GetKHtheoNV(frmLogin.mail);
            dataGridView1.Columns["id"].Visible = false;
        }
    }
}
