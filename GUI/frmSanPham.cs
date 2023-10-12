﻿using System;
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
            
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            MoTa moTa = new MoTa();
            string ma = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            moTa = SanPhamBLL.getMoTa(ma);
            //Console.WriteLine(moTa.HieuNang.ToString());
            txtHieuNang.Text = moTa.HieuNang.ToString();
            txtKichThuoc.Text = moTa.KichThuoc.ToString();
            txtTrongLuong.Text = moTa.TrongLuong.ToString();
            txtMa.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtGia.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtSLT.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtHang.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
