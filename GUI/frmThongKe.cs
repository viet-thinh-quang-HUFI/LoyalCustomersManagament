using BLL;
using DTO;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class frmThongKe : Form
    {
        HoaDonBLL hoaDonBLL = new HoaDonBLL();
        KhachHangBLL khachHangBLL = new KhachHangBLL();
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

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            List<XepHang> dataSource = new List<XepHang>();

            dataSource.Add(new XepHang() { Ten = "Thường", DiemXepHang = "0"});
            dataSource.Add(new XepHang() { Ten = "Bạc", DiemXepHang = "1"});
            dataSource.Add(new XepHang() { Ten = "Vàng", DiemXepHang = "2"});

            cbCapBac.DataSource = dataSource;
            cbCapBac.DisplayMember = "Ten";
            cbCapBac.ValueMember = "DiemXepHang";

            cbCapBac.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cbCapBac_SelectedValueChanged(object sender, EventArgs e)
        {
            string d = cbCapBac.SelectedValue.ToString();
            txtDiem.Text = d;
            dtgKhachHang.DataSource = khachHangBLL.GetKHThongKe(d);
            dtgKhachHang.Columns["id"].Visible = false;
        }

        private void dtgKhachHang_MouseClick(object sender, MouseEventArgs e)
        {
            txtTen.Text = dtgKhachHang.CurrentRow.Cells[2].Value.ToString();
            txtDiem.Text = dtgKhachHang.CurrentRow.Cells[6].Value.ToString();
        }
    }
}
