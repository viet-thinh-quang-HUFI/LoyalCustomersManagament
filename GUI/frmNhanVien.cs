using BLL;
using System;
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
        public frmNhanVien()
        {
            InitializeComponent();
            this.Load += FrmNhanVien_Load;
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            DisplayDGV_NhanVien();
        }

        private void DisplayDGV_NhanVien()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nhân viên");
            dt.Columns.Add("Họ và tên");
            dt.Columns.Add("Email");
            dt.Columns.Add("Mật khẩu");
            dt.Columns.Add("Chỉ số KPI");
            dt.Columns.Add("Avatar");

            nhanVienBLL.GetNhanVien().ForEach(n =>
            {
                dt.Rows.Add(n.MaNV, n.HotenNV, n.EmailNV, n.Matkhau, n.KPI, n.Avatar);
            });

            dgvNhanVien.DataSource = dt;
            dgvNhanVien.Columns["Mật khẩu"].Visible = false;
            dgvNhanVien.Columns["Avatar"].Visible = false;
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.AllowUserToAddRows = false;

            nhanVienBLL.GetListKHsOfNV("NV01");
        }

        private void dgvNhanVien_MouseClick(object sender, MouseEventArgs e)
        {
            tbMaNV.Text = dgvNhanVien.CurrentRow.Cells[0].Value.ToString();
            tbHoTen.Text = dgvNhanVien.CurrentRow.Cells[1].Value.ToString();
            tbEmail.Text = dgvNhanVien.CurrentRow.Cells[2].Value.ToString();
            tbMauKhau.Text = dgvNhanVien.CurrentRow.Cells[3].Value.ToString();
            tbKPI.Text = dgvNhanVien.CurrentRow.Cells[4].Value.ToString();

            RoundImageChanger(pbAvatar, dgvNhanVien.CurrentRow.Cells[5].Value.ToString());

            DisplayDGV_KHsOfNV();
        }

        private void DisplayDGV_KHsOfNV()
        {
            dgvKhachHang.DataSource = nhanVienBLL.GetListKHsOfNV("");
        }

        private void RoundImageChanger(PictureBox pb, String url)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, pb.Width - 3, pb.Height - 3);
            Region rg = new Region(gp);
            pb.Region = rg;
            pb.LoadAsync(url);
        }
    }
}
