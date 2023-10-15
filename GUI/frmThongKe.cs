using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmThongKe : Form
    {
        HoaDonBLL hoaDonBLL = new HoaDonBLL();
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime ngaybd = dtNgayBD.Value.Date;
            DateTime ngaykt = dtNgayKT.Value.Date;
            if (ngaybd > ngaykt)
            {
                MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc");
            }
            else
            {
                dataGridView1.DataSource = hoaDonBLL.GetHoaDon(ngaybd, ngaykt);
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.Columns["NgayLap"].Visible = false;
            }
            //label6.Text = dtNgayBD.Value.Date.ToString();
        }


    }
}
