using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmNhanVien : Form
    {
        NhanVienBLL nhanVienBLL = new NhanVienBLL();
        KhachHangBLL khachHangBLL = new KhachHangBLL();
        public static List<NhanVien> listTopKPI = new List<NhanVien>();
        public frmNhanVien()
        {
            InitializeComponent();
            this.Load += FrmNhanVien_Load;
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            DisplayDGV_NhanVien();

            dgvNhanVien.Rows[0].Selected = true;
            dgvNhanVien.Columns["Mật khẩu"].Visible = false;
            dgvNhanVien.Columns["Avatar"].Visible = false;
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.AllowUserToAddRows = false;

            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.AllowUserToAddRows = false;

            rdoTen.Checked = true;
        }

        private void DisplayDGV_NhanVien(String query = "", int type = 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nhân viên");
            dt.Columns.Add("Họ và tên");
            dt.Columns.Add("Email");
            dt.Columns.Add("Mật khẩu");
            dt.Columns.Add("Chỉ số KPI");
            dt.Columns.Add("Avatar");
            dt.Columns.Add("Is Admin");

            if (query == String.Empty)
            {
                nhanVienBLL.GetNhanVien().ForEach(n =>
                {
                    dt.Rows.Add(n.MaNV, n.HotenNV, n.EmailNV, n.Matkhau, n.KPI, n.Avatar, n.IsAdmin);
                });

            }
            else
            {
                if (type == 1)
                {
                    nhanVienBLL.SearchByTen(query).ForEach(n =>
                    {
                        dt.Rows.Add(n.MaNV, n.HotenNV, n.EmailNV, n.Matkhau, n.KPI, n.Avatar, n.IsAdmin);
                    });

                }
                else if (type == 2)
                {
                    nhanVienBLL.SearchByEmail(query).ForEach(n =>
                    {
                        dt.Rows.Add(n.MaNV, n.HotenNV, n.EmailNV, n.Matkhau, n.KPI, n.Avatar, n.IsAdmin);
                    });
                }
            }
            dgvNhanVien.DataSource = dt;
        }

        private void dgvNhanVien_MouseClick(object sender, MouseEventArgs e)
        {
            tbMaNV.Text = dgvNhanVien.CurrentRow.Cells[0].Value.ToString();
            tbHoTen.Text = dgvNhanVien.CurrentRow.Cells[1].Value.ToString();
            tbEmail.Text = dgvNhanVien.CurrentRow.Cells[2].Value.ToString();
            tbMauKhau.Text = dgvNhanVien.CurrentRow.Cells[3].Value.ToString();
            tbKPI.Text = dgvNhanVien.CurrentRow.Cells[4].Value.ToString();

            RoundImageChanger(pbAvatar, dgvNhanVien.CurrentRow.Cells[5].Value.ToString());

            if (dgvNhanVien.CurrentRow.Cells[6].Value.ToString() == "True")
                cbAdmin.Checked = true;
            else
                cbAdmin.Checked = false;


            DisplayDGV_KHsOfNV();
        }

        private void DisplayDGV_KHsOfNV()
        {
            dgvKhachHang.DataSource = nhanVienBLL.GetListKHsOfNV(dgvNhanVien.CurrentRow.Cells[0].Value.ToString());
            dgvKhachHang.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKhachHang.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKhachHang.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKhachHang.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKhachHang.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvKhachHang.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void RoundImageChanger(PictureBox pb, String url)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, pb.Width - 3, pb.Height - 3);
            Region rg = new Region(gp);
            pb.Region = rg;
            if (url != String.Empty)
                pb.LoadAsync(url);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.MaNV = tbMaNV.Text;
            nv.HotenNV = tbHoTen.Text;
            nv.EmailNV = tbEmail.Text;
            nv.Matkhau = tbMauKhau.Text;
            nv.KPI = Convert.ToInt16(tbKPI.Value);

            bool check = Convert.ToBoolean(cbAdmin.CheckState);
            nv.IsAdmin = check;

            var rs = nhanVienBLL.AddNhanVien(nv);
            if (rs == 1)
            {
                MessageBox.Show("Mã, Email, Mật khẩu không để trống", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rs == 2)
            {
                MessageBox.Show("Email sai định dạng", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rs == 3)
            {
                MessageBox.Show("Trùng mã", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Đăng ký thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayDGV_NhanVien();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.MaNV = tbMaNV.Text;
            nv.HotenNV = tbHoTen.Text;
            nv.EmailNV = tbEmail.Text;
            nv.Matkhau = tbMauKhau.Text;
            nv.KPI = Convert.ToInt16(tbKPI.Value);
            nv.IsAdmin = cbAdmin.Checked;

            bool check = Convert.ToBoolean(cbAdmin.CheckState);
            nv.IsAdmin = check;

            var rs = nhanVienBLL.UpdateNhanVien(nv);
            if (rs == 1)
            {
                MessageBox.Show("Mã, Email, Mật khẩu không để trống", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rs == 2)
            {
                MessageBox.Show("Email sai định dạng", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rs == 3)
            {
                MessageBox.Show("Mã không tồn tại", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Cập nhật thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayDGV_NhanVien();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String maNV = tbMaNV.Text;

            var rs = nhanVienBLL.DeleteNhanVien(maNV);
            if (rs == 1)
            {
                MessageBox.Show("Mã không để trống", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rs == 2)
            {
                MessageBox.Show("Mã không tồn tại", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Xóa thành công!", "Loyal Customers Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayDGV_NhanVien();
            }
        }

        private void btnTopKPI_Click(object sender, EventArgs e)
        {
            DataTopAPI();
            DialogTopKPI dialog = new DialogTopKPI();
            dialog.ShowDialog();
        }

        public void DataTopAPI()
        {
            listTopKPI = nhanVienBLL.GetTopKPI();
        }

        private void icButtonReload_Click(object sender, EventArgs e)
        {
            DisplayDGV_NhanVien();
            DisplayDGV_KHsOfNV();
        }

        private void tbTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (rdoTen.Checked)
            {
                string query = tbTimKiem.Text.ToLower().Trim();
                DisplayDGV_NhanVien(query, 1);
            }
            else
            {
                string query = tbTimKiem.Text.ToLower().Trim();
                DisplayDGV_NhanVien(query, 2);
            }
        }
    }
}
