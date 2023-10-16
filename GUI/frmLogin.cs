using BLL;
using DTO;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmLogin : Form
    {
        NhanVienBLL nhanVienBLL = new NhanVienBLL();
        public static string mail = "";
        public static Boolean isAdmin = false;


        public frmLogin()
        {
            InitializeComponent();
            this.Load += FrmLogin_Load;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            tbAccountName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.EmailNV = tbAccountName.Text;
            nhanVien.Matkhau = tbPassword.Text;

            Byte result = nhanVienBLL.Login(nhanVien);

            if (result == 0)
            {

                frmLoading f = new frmLoading();
                f.Show();
                mail = tbAccountName.Text;
                isAdmin = nhanVienBLL.CheckIsAdmin(tbAccountName.Text);
                this.Hide();
            }
            else if (result == 1)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
            }
            else
            {
                MessageBox.Show("Tài khoản/mật khẩu không hợp lệ!");
            }
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            frmResetPassword f = new frmResetPassword();
            f.Show();
            this.Hide();
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
