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
            dataGridView1.DataSource = SanPhamBLL.getSanPham();
            //SanPhamBLL.getMoTa();
            MoTa moTa = new MoTa();
            moTa = SanPhamBLL.getMoTa("SP12");
            //Console.WriteLine(moTa.HieuNang.ToString());
            label1.Text = moTa.HieuNang.ToString();
            label2.Text = moTa.KichThuoc.ToString();
            label3.Text = moTa.TrongLuong.ToString();
        }
    }
}
