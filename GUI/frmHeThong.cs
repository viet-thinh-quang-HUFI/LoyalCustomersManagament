using BLL;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using MongoDB.Bson.IO;

namespace GUI
{

    public partial class frmHeThong : Form
    {
        private const bool V = false;
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
            displayDatabaseSize();

            ShowSizeCollection(lblSizeHang, lblNumDOCHang, "Hang");
            ShowSizeCollection(lblSizeHD, lblNumDOCHD, "HoaDon");
            ShowSizeCollection(lblSizeKH, lblNumDOCKH, "KhachHang");
            ShowSizeCollection(lblSizeNV, lblNumDOCNV, "NhanVien");
            ShowSizeCollection(lblSizeSP, lblNumDOCSP, "SanPham");
        }

        private void displayDatabaseSize()
        {
            labelDBSize.Text = Math.Round(heThongBLL.GetDatabaseSize(), 1).ToString() + " KB";
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
            var fileName = GetFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = hangBLL.ImportHang(fileName, (callBack =>
                    {
                        if (callBack == true)
                        {
                            MessageBox.Show("Import dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowSizeCollection(lblSizeHang, lblNumDOCHang, "Hang");
                            displayDatabaseSize();
                        }
                        else
                        {
                            MessageBox.Show("Import dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));
            }
        }

        private void btnImportHD_Click(object sender, EventArgs e)
        {
            var fileName = GetFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = hoaDonBLL.ImportHoaDon(fileName, (callBack =>
                    {
                        if (callBack == true)
                        {
                            MessageBox.Show("Import dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowSizeCollection(lblSizeHD, lblNumDOCHD, "HoaDon");
                            displayDatabaseSize();
                        }
                        else
                        {
                            MessageBox.Show("Import dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));
            }
        }

        private void btnImportKH_Click(object sender, EventArgs e)
        {
            var fileName = GetFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = khachHangBLL.ImportKhachHang(fileName, (callBack =>
                    {
                        if (callBack == true)
                        {
                            MessageBox.Show("Import dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowSizeCollection(lblSizeKH, lblNumDOCKH, "KhachHang");
                            displayDatabaseSize();
                        }
                        else
                        {
                            MessageBox.Show("Import dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));
            }
        }

        private void btnImportNV_Click(object sender, EventArgs e)
        {
            var fileName = GetFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = nhanVienBLL.ImportNhanVien(fileName, (callBack =>
                    {
                        if (callBack == true)
                        {
                            MessageBox.Show("Import dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowSizeCollection(lblSizeNV, lblNumDOCNV, "NhanVien");
                            displayDatabaseSize();
                        }
                        else
                        {
                            MessageBox.Show("Import dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));
            }
        }

        private void btnImportSP_Click(object sender, EventArgs e)
        {
            var fileName = GetFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = sanPhamBLL.ImportSanPham(fileName, (callBack =>
                    {
                        if (callBack == true)
                        {
                            MessageBox.Show("Import dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowSizeCollection(lblSizeSP, lblNumDOCSP, "SanPham");
                            displayDatabaseSize();
                        }
                        else
                        {
                            MessageBox.Show("Import dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));
            }
        }

        private void btnExportHang_Click(object sender, EventArgs e)
        {
            var fileName = SaveFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = hangBLL.ExportHang(fileName, (callBack =>
                    {
                        if (callBack == true)
                        {
                            MessageBox.Show("Export dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Export dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));
            }
        }

        private void btnExportHD_Click(object sender, EventArgs e)
        {
            var fileName = SaveFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = hoaDonBLL.ExportHoaDon(fileName, (callBack =>
                {
                    if (callBack == true)
                    {
                        MessageBox.Show("Export dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Export dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
            }
        }

        private void btnExportKH_Click(object sender, EventArgs e)
        {
            var fileName = SaveFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = khachHangBLL.ExportKhachHang(fileName, (callBack =>
                {
                    if (callBack == true)
                    {
                        MessageBox.Show("Export dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Export dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
            }
        }

        private void btnExportNV_Click(object sender, EventArgs e)
        {
            var fileName = SaveFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = nhanVienBLL.ExportNhanVien(fileName, (callBack =>
                {
                    if (callBack == true)
                    {
                        MessageBox.Show("Export dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Export dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
            }
        }

        private void btnExportSP_Click(object sender, EventArgs e)
        {
            var fileName = SaveFileName();
            if (fileName == null)
            {
                return;
            }
            else
            {
                _ = sanPhamBLL.ExportSanPham(fileName, (callBack =>
                    {
                        if (callBack == true)
                        {
                            MessageBox.Show("Export dữ liệu thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Export dữ liệu thất bại!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));
            }
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
                            displayDatabaseSize();
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
                            displayDatabaseSize();
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
                            displayDatabaseSize();
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
                            displayDatabaseSize();
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
                            displayDatabaseSize();
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

        private void ReloadAll()
        {
            ShowSizeCollection(lblSizeHang, lblNumDOCHang, "Hang");
            ShowSizeCollection(lblSizeHD, lblNumDOCHD, "HoaDon");
            ShowSizeCollection(lblSizeKH, lblNumDOCKH, "KhachHang");
            ShowSizeCollection(lblSizeNV, lblNumDOCNV, "NhanVien");
            ShowSizeCollection(lblSizeSP, lblNumDOCSP, "SanPham");
            displayDatabaseSize();
        }

        private void icButtonReload_Click(object sender, EventArgs e)
        {
            ReloadAll();
        }

        private String GetFileName()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.json|";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }

        private String SaveFileName()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
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
            : gb > 1 ? string.Format("{0} GB", gb)
            : mb > 1 ? string.Format("{0} MB", mb)
            : kb > 1 ? string.Format("{0} KB", kb)
            : string.Format("{0} byte", Math.Round((double)value, decimalPlaces));
        return size;
    }
}
