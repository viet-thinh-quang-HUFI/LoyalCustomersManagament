using BLL;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmHeThong : Form
    {
        HeThongBLL heThongBLL = new HeThongBLL();
        HangBLL hangBLL = new HangBLL();
        HoaDonBLL hoaDonBLL = new HoaDonBLL();
        KhachHangBLL khachHangBLL = new KhachHangBLL();
        NhanVienBLL nhanVienBLL = new NhanVienBLL();
        SanPhamBLL sanPhamBLL = new SanPhamBLL();

        public frmHeThong()
        {
            InitializeComponent();
            this.Load += FrmHeThong_Load;
        }

        private void FrmHeThong_Load(object sender, EventArgs e)
        {
            cboDisk.Items.AddRange(DriveInfo.GetDrives());
            cboCollectionName.DataSource = heThongBLL.GetCollectionName();
            labelDBSize.Text = Math.Round(heThongBLL.GetDatabaseSize() / 1024, 1).ToString();

            ShowSizeCollection(lblSizeHang, lblNumDOCHang, "Hang");
            ShowSizeCollection(lblSizeHD, lblNumDOCHD, "HoaDon");
            ShowSizeCollection(lblSizeKH, lblNumDOCKH, "KhachHang");
            ShowSizeCollection(lblSizeNV, lblNumDOCNV, "NhanVien");
            ShowSizeCollection(lblSizeSP, lblNumDOCSP, "SanPham");
        }

        private void cboCollectionName_SelectedValueChanged(object sender, EventArgs e)
        {
            this.dgvCollectionDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCollectionDetails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;


            dgvCollectionDetails.DataSource = heThongBLL.GetCollectionDetails(cboCollectionName.SelectedValue.ToString());
        }

        private void cboDisk_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriveInfo driveInfo = new DriveInfo(cboDisk.Text.Substring(0, 1));
            lblType.Text = driveInfo.DriveType.ToString();
            if (driveInfo.IsReady)
            {
                lblFormat.Text = driveInfo.DriveFormat;
                lblAvailableFreeSpace.Text = driveInfo.AvailableFreeSpace.ToPrettySize();
                lblTotalFreeSize.Text = driveInfo.TotalFreeSpace.ToPrettySize();
                lblTotalSize.Text = driveInfo.TotalSize.ToPrettySize();
            }
            else
            {
                lblFormat.Text = string.Empty;
                lblAvailableFreeSpace.Text = string.Empty;
                lblTotalFreeSize.Text = string.Empty;
                lblTotalSize.Text = string.Empty;
            }
        }

        private void btnImportHang_Click(object sender, EventArgs e)
        {

        }

        private void btnImportHD_Click(object sender, EventArgs e)
        {

        }

        private void btnImportKH_Click(object sender, EventArgs e)
        {

        }

        private void btnImportNV_Click(object sender, EventArgs e)
        {

        }

        private void btnImportSP_Click(object sender, EventArgs e)
        {

        }

        private void btnExportHang_Click(object sender, EventArgs e)
        {

        }

        private void btnExportHD_Click(object sender, EventArgs e)
        {

        }

        private void btnExportKH_Click(object sender, EventArgs e)
        {

        }

        private void btnExportNV_Click(object sender, EventArgs e)
        {

        }

        private void btnExportSP_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteHang_Click(object sender, EventArgs e)
        {
            DialogResult drQ = MessageBox.Show("Bạn có chắc chắn muốn xoá tất cả?",
                      "Loyal Customers Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (drQ)
            {
                case DialogResult.Yes:
                    {
                        var result = hangBLL.DeleteAllHang();
                        if (result == 0)
                        {
                            MessageBox.Show("Xoá thành công", "Loyal Customers Management", MessageBoxButtons.OK);
                            ShowSizeCollection(lblSizeHang, lblNumDOCHang, "Hang");
                        }
                        else
                        {
                            MessageBox.Show("Có gì đó không ổn!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case DialogResult.No:
                    {
                        break;
                    }
            }
        }

        private void btnDeleteHD_Click(object sender, EventArgs e)
        {
            DialogResult drQ = MessageBox.Show("Bạn có chắc chắn muốn xoá tất cả?",
                      "Loyal Customers Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (drQ)
            {
                case DialogResult.Yes:
                    {
                        var result = hoaDonBLL.DeleteAllHoaDon();
                        if (result == 0)
                        {
                            MessageBox.Show("Xoá thành công", "Loyal Customers Management", MessageBoxButtons.OK);
                            ShowSizeCollection(lblSizeHD, lblNumDOCHD, "HoaDon");
                        }
                        else
                        {
                            MessageBox.Show("Có gì đó không ổn!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case DialogResult.No:
                    {
                        break;
                    }
            }
        }

        private void btnDeleteKH_Click(object sender, EventArgs e)
        {
            DialogResult drQ = MessageBox.Show("Bạn có chắc chắn muốn xoá tất cả?",
                      "Loyal Customers Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (drQ)
            {
                case DialogResult.Yes:
                    {
                        var result = khachHangBLL.DeleteAllKhachHang();
                        if (result == 0)
                        {
                            MessageBox.Show("Xoá thành công", "Loyal Customers Management", MessageBoxButtons.OK);
                            ShowSizeCollection(lblSizeKH, lblNumDOCKH, "KhachHang");
                        }
                        else
                        {
                            MessageBox.Show("Có gì đó không ổn!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case DialogResult.No:
                    {
                        break;
                    }
            }
        }

        private void btnDeleteNV_Click(object sender, EventArgs e)
        {
            DialogResult drQ = MessageBox.Show("Bạn có chắc chắn muốn xoá tất cả?",
                      "Loyal Customers Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (drQ)
            {
                case DialogResult.Yes:
                    {
                        var result = nhanVienBLL.DeleteAllNhanVien();
                        if (result == 0)
                        {
                            MessageBox.Show("Xoá thành công", "Loyal Customers Management", MessageBoxButtons.OK);
                            ShowSizeCollection(lblSizeNV, lblNumDOCNV, "NhanVien");
                        }
                        else
                        {
                            MessageBox.Show("Có gì đó không ổn!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case DialogResult.No:
                    {
                        break;
                    }
            }
        }

        private void btnDeleteSP_Click(object sender, EventArgs e)
        {
            DialogResult drQ = MessageBox.Show("Bạn có chắc chắn muốn xoá tất cả?",
                      "Loyal Customers Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (drQ)
            {
                case DialogResult.Yes:
                    {
                        var result = sanPhamBLL.DeleteAllSanPham();
                        if (result == 0)
                        {
                            MessageBox.Show("Xoá thành công", "Loyal Customers Management", MessageBoxButtons.OK);
                            ShowSizeCollection(lblSizeSP, lblNumDOCSP, "SanPham");
                        }
                        else
                        {
                            MessageBox.Show("Có gì đó không ổn!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case DialogResult.No:
                    {
                        break;
                    }
            }
        }

        private void ShowSizeCollection(Label size, Label count, String collectionName)
        {
            Dictionary<String, Object> result = heThongBLL.GetCollectionSizeAndCount(collectionName);

            size.Text = result["size"].ToString();
            count.Text = result["count"].ToString();
        }
    }
}

public static class ConverterExtension
{
    private const long Kb = 1024;
    private const long Mb = Kb * 1024;
    private const long Gb = Mb * 1024;
    private const long Tb = Gb * 1024;

    public static string ToPrettySize(this long value, int decimalPlaces = 0)
    {
        var tb = Math.Round((double)value / Tb, decimalPlaces);
        var gb = Math.Round((double)value / Gb, decimalPlaces);
        var mb = Math.Round((double)value / Mb, decimalPlaces);
        var kb = Math.Round((double)value / Kb, decimalPlaces);
        string size = tb > 1 ? string.Format("{0}Tb", tb)
            : gb > 1 ? string.Format("{0} Gb", gb)
            : mb > 1 ? string.Format("{0} Mb", mb)
            : kb > 1 ? string.Format("{0} Kb", kb)
            : string.Format("{0} byte", Math.Round((double)value, decimalPlaces));
        return size;
    }
}
