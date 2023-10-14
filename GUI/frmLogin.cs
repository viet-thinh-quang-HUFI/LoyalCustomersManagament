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

        public frmLogin()
        {
            InitializeComponent();
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

            if(result == 0)
            {
                frmLoading f = new frmLoading();
                f.Show();
                mail = tbAccountName.Text;
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
    }
}
