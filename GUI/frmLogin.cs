using BLL;
using DTO;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmLogin : Form
    {
        NhanVienBLL nhanVienBLL = new NhanVienBLL();

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

            Boolean result = nhanVienBLL.Login(nhanVien);

            if(result == true)
            {
                frmLoading f = new frmLoading();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tài khoản mật khẩu không hợp lệ!");
            }
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {

        }
    }
}
